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
            int childCount = Random.Range(1, maxChildren + 1);

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
            Destroy(currentRoomInstance);
        }

        currentRoomIndex = index;
        RoomData roomData = dungeonData.Rooms[index];
        currentRoomInstance = Instantiate(roomData.RoomPrefab);

        //Set player to SpawnPoint
        Transform spawnPoint = currentRoomInstance.transform.Find("PlayerSpawnPoint");
        if (spawnPoint != null)
        {
            playerTransform.position = spawnPoint.position;
        }


        // Configure doors based on generated layout
        ConfigureDoors(index, entryWall);


        //Handle room state
        bool alreadyCleared = clearedRooms.Contains(index);

        if (roomData.RoomType == RoomType.Start || alreadyCleared)
        {
            //Debug.Log("DungeonManager LoadRoom Start Room: " + currentRoomInstance.gameObject.name);
            RoomManager roomManager = currentRoomInstance.GetComponent<RoomManager>();
            if (roomManager != null)
            {
                roomManager.ClearImmediately();
            }

            if (alreadyCleared)
            {
                SpawnEnemy spawner = currentRoomInstance.GetComponentInChildren<SpawnEnemy>();
                if(spawner != null)
                {
                    spawner.enabled = false;
                }
            }
        }
    }

    private void ConfigureDoors(int roomIndex, WallSide? entryWall)
    {
        DungeonDoor[] allDoors = currentRoomInstance.GetComponentsInChildren<DungeonDoor>(true);

        List<int> forwardTargets = forwardConnections[roomIndex];
        int parent = parentRoom[roomIndex];
        WallSide? backDoorWall = entryWall.HasValue ? GetOpposite(entryWall.Value) : null;

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


        // --- Configure back doors ---
        // Activate only the back door on the opposite wall of entry
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
        // Skip any forward door on the same wall as the active back door
        int forwardAssigned = 0;
        foreach (DungeonDoor forwardDoor in forwardDoors)
        {
            // Don't put a forward door on the wall where the back door is
            if (backDoorWall.HasValue && forwardDoor.WallSide == backDoorWall.Value)
            {
                forwardDoor.gameObject.SetActive(false);
                continue;
            }
            
            if (forwardAssigned < forwardTargets.Count)
            {
                forwardDoor.gameObject.SetActive(true);
                forwardDoor.SetTarget(forwardTargets[forwardAssigned]);
                forwardAssigned++;
            }
            else
            {
                forwardDoor.gameObject.SetActive(false);
            }

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
