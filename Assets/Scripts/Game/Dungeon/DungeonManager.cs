using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonDataSO dungeonData;
    [SerializeField] private DungeonProgressSO dungeonProgress;

    [Header("Events")]
    [SerializeField] private GameEventSO DungeonManager_onDungeonCompleted;
    [SerializeField] private GameEventSO RoomManager_onRoomCleared;

    private int currentRoomIndex;
    private GameObject currentRoomInstance;
    private Transform playerTransform;


    //Generated layout
    private List<int>[] forwardConnections;
    private int[] parentRoom;
    private HashSet<int> clearedRooms = new HashSet<int>();
    private Dictionary<int, Dictionary<int, WallSide>> storedDoorAssignments = new();
    private Dictionary<int, WallSide> storedBackDoorWalls = new();


    private void OnEnable()
    {
        DungeonManager_onDungeonCompleted.Subscribe(OnDungeonCompleted);
        RoomManager_onRoomCleared.Subscribe(OnRoomCleared);
    }

    private void OnDisable()
    {
        DungeonManager_onDungeonCompleted.UnSubscribe(OnDungeonCompleted);
        RoomManager_onRoomCleared.UnSubscribe(OnRoomCleared);
    }

    private void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        playerTransform = player.transform;

        GenerateLayout();
        LoadRoom(0, null);
    }

    #region Layout Generation
    private void GenerateLayout()
    {
        int roomCount = dungeonData.Rooms.Length;   //6
        int bossIndex = roomCount - 1;              //5

        forwardConnections = new List<int>[roomCount];
        parentRoom = new int[roomCount];

        for (int i = 0; i < roomCount; i++)
        {
            forwardConnections[i] = new List<int>();
            parentRoom[i] = -1;
        }

        // Shuffle fight room indices [1, 2, 3, 4]
        List<int> fightRooms = new List<int>();
        for (int i = 1; i < bossIndex; i++)
        {
            fightRooms.Add(i);
        }
        Shuffle(fightRooms);

        Queue<int> remaining = new Queue<int>(fightRooms);
        int spineRoom = 0; //Start room is first spine room

        while (remaining.Count > 0)
        {
            int maxChildren = Mathf.Min(remaining.Count, 3);
            //push StartRoom towards less doors
            int childCount = Mathf.Min(Random.Range(1, maxChildren + 1), Random.Range(1, maxChildren + 1));

            List<int> children = new List<int>();
            for (int i = 0; i < childCount; i++)
            {
                children.Add(remaining.Dequeue());
            }

            // Pick one child as the spine (continues toward Boss)
            int spineChildIndex = Random.Range(0, children.Count);
            int spineChild = children[spineChildIndex];

            // All children are forward connections from the current spine room
            foreach (int child in children)
            {
                forwardConnections[spineRoom].Add(child);
                parentRoom[child] = spineRoom;
            }

            // Only the spine child continues; the rest are dead ends
            spineRoom = spineChild;
        }

        // Last spine room connects to Boss
        forwardConnections[spineRoom].Add(bossIndex);
        parentRoom[bossIndex] = spineRoom;

        //Debug log
        Debug.Log("=== Dungeon Layout Generated ===");
        for (int i = 0; i < roomCount; i++)
        {
            string forward = forwardConnections[i].Count > 0
                            ? string.Join(", ", forwardConnections[i]) : "none (dead end)";
            Debug.Log($"Room {i} ({dungeonData.Rooms[i].RoomType}): forward=[{forward}], parent={parentRoom[i]}");
        }
    }

    private void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    #endregion


    #region Room Loading

    // entryWall = which wall the player came through (null for Start room)
    public void LoadRoom(int index, WallSide? entryWall)
    {
        if(currentRoomInstance != null)
        {
            currentRoomInstance.SetActive(false); // triggers OnDisable --> unsubscribes events immediately
            Destroy(currentRoomInstance);
        }

        currentRoomIndex = index;
        RoomData roomData = dungeonData.Rooms[index];
        currentRoomInstance = Instantiate(roomData.RoomPrefab);
        Debug.Log("jelenleg szoba: " + currentRoomInstance.gameObject.name);

        //Set player to SpawnPoint
        playerTransform.position = GetSpawnPosition(entryWall);

        // Configure doors based on generated layout
        ConfigureDoors(index, entryWall);


        //Handle room state
        bool alreadyCleared = clearedRooms.Contains(index);

        if (alreadyCleared)
        {
            RoomManager roomManager = currentRoomInstance.GetComponent<RoomManager>();
            if (roomManager != null)
            {
                roomManager.ClearImmediately();
            }

            SpawnEnemy spawner = currentRoomInstance.GetComponentInChildren<SpawnEnemy>();
            if (spawner != null)
            {
                spawner.enabled = false;
            }

            if (roomData.RoomType == RoomType.Start)
            {
                foreach(var weapon in currentRoomInstance.GetComponentsInChildren<Weaponchoice>(true))
                {
                    (weapon as MonoBehaviour)?.gameObject.SetActive(false);
                }
            }
        }
    }

    private Vector2 GetSpawnPosition(WallSide? entrywall)
    {
        if (!entrywall.HasValue) return Vector2.zero;

        return entrywall.Value switch
        {
            WallSide.Left => new Vector2(25f, 0f),
            WallSide.Right => new Vector2(-25f, 0),
            WallSide.Top => new Vector2(0f, -25f),
            WallSide.Bottom => new Vector2(0f, 25f),
            _ => Vector2.zero,
        };
    }

    private void ConfigureDoors(int roomIndex, WallSide? entryWall)
    {
        DungeonDoor[] allDoors = currentRoomInstance.GetComponentsInChildren<DungeonDoor>(true);

        List<int> forwardTargets = forwardConnections[roomIndex];
        int parent = parentRoom[roomIndex];
        List<DungeonDoor> forwardDoors = new List<DungeonDoor>();
        List<DungeonDoor> backDoors = new List<DungeonDoor>();

        foreach (DungeonDoor door in allDoors)
        {
            if (door.DoorType == DoorType.Forward)
            {
                forwardDoors.Add(door);
            }
            else if (door.DoorType == DoorType.Back)
            {
                backDoors.Add(door);
            }
        }

        // --- Resolve back door wall (use stored on revisit) ---
        WallSide? backDoorWall;
        if (storedBackDoorWalls.TryGetValue(roomIndex, out WallSide storedWall))
        {
            backDoorWall = storedWall;
        }
        else if (entryWall.HasValue)
        {
            backDoorWall = GetOpposite(entryWall.Value);
            storedBackDoorWalls[roomIndex] = backDoorWall.Value;
        }
        else
        {
            backDoorWall = null;
        }

        // --- Configure back doors ---
        // Activate only the back door on the stored wall
        foreach (DungeonDoor backDoor in backDoors)
        {
            if (backDoorWall.HasValue && backDoor.WallSide == backDoorWall.Value && parent >= 0)
            {
                backDoor.gameObject.SetActive(true);
                backDoor.SetTarget(parent);
            }
            else
            {
                backDoor.gameObject.SetActive(false);
            }
        }

        // --- Configure forward doors ---
        if (storedDoorAssignments.TryGetValue(roomIndex, out var stored))
        {
            // Build wall --> target lookup once
            Dictionary<WallSide, int> wallToTarget = new Dictionary<WallSide, int>();
            foreach (var kvp in stored)
                wallToTarget[kvp.Value] = kvp.Key;

            // Then for each door:
            foreach (DungeonDoor forwardDoor in forwardDoors)
            {
                if (wallToTarget.TryGetValue(forwardDoor.WallSide, out int target))
                {
                    forwardDoor.gameObject.SetActive(true);
                    forwardDoor.SetTarget(target);

                    if (dungeonData.Rooms[target].RoomType == RoomType.Boss)
                    {
                        forwardDoor.SetAsBossDoor();
                    }
                }
                else
                {
                    forwardDoor.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // FIRST VISIT: assign sequentially, store mapping
            Dictionary<int, WallSide> assignments = new Dictionary<int, WallSide>();
            int forwardAssigned = 0;


            // Shuffle forward doors so active doors are randomized
            for (int i = forwardDoors.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (forwardDoors[i], forwardDoors[j]) = (forwardDoors[j], forwardDoors[i]);
            }


            foreach (DungeonDoor forwardDoor in forwardDoors)
            {
                if (backDoorWall.HasValue && forwardDoor.WallSide == backDoorWall.Value)
                {
                    forwardDoor.gameObject.SetActive(false);
                    continue;
                }

                //no bottom door on Start room
                if (dungeonData.Rooms[roomIndex].RoomType == RoomType.Start
                    && forwardDoor.WallSide == WallSide.Bottom)
                {
                    forwardDoor.gameObject.SetActive(false);
                    continue;
                }

                if (forwardAssigned < forwardTargets.Count)
                {
                    forwardDoor.gameObject.SetActive(true);
                    forwardDoor.SetTarget(forwardTargets[forwardAssigned]);
                    if (dungeonData.Rooms[forwardTargets[forwardAssigned]].RoomType == RoomType.Boss)
                    {
                        forwardDoor.SetAsBossDoor();
                    }
                    assignments[forwardTargets[forwardAssigned]] = forwardDoor.WallSide;
                    forwardAssigned++;
                }
                else
                {
                    forwardDoor.gameObject.SetActive(false);
                }
            }
            storedDoorAssignments[roomIndex] = assignments;
        }
    }

    private WallSide GetOpposite(WallSide side)
    {
        return side switch
        {
            WallSide.Top => WallSide.Bottom,
            WallSide.Bottom => WallSide.Top,
            WallSide.Right => WallSide.Left,
            WallSide.Left => WallSide.Right,
            _ => WallSide.Bottom,
        };
    }
    #endregion


    private void OnRoomCleared()
    {
        clearedRooms.Add(currentRoomIndex);
    }

    private void OnDungeonCompleted()
    {
        dungeonProgress.CompleteDungeon(dungeonData.DungeonIndex);
    }
}
