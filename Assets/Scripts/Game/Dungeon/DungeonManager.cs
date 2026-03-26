using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonDataSO dungeonData;
    [SerializeField] private DungeonProgressSO dungeonProgress;

    [Header("Events")]
    [SerializeField] private GameEventSO DungeonManager_onDungeonCompleted;
    [SerializeField] private GameEventSO PlayerDeathEvent;
    [SerializeField] private GameEventSO ReturnToHubEvent;

    private int currentRoomIndex;
    private GameObject currentRoomInstance;
    private Transform playerTransform;

    private void OnEnable()
    {
        DungeonManager_onDungeonCompleted.Subscribe(OnDungeonCompleted);
    }

    private void OnDisable()
    {
        DungeonManager_onDungeonCompleted.UnSubscribe(OnDungeonCompleted);
    }

    private void Start()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        playerTransform = player.transform;
        LoadRoom(0);
    }

    public void LoadRoom(int index)
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


        if (roomData.RoomType == RoomType.Start)
        {
            RoomManager roomManager = currentRoomInstance.GetComponent<RoomManager>();
            if (roomManager != null)
            {
                roomManager.ClearImmediately();
            }
        }
        else if (roomData.RoomType == RoomType.Normal)
        {
            RandomizeDoors(roomData);
        }
    }

    private void RandomizeDoors(RoomData roomData)
    {
        DungeonDoor[] doors = currentRoomInstance.GetComponentsInChildren<DungeonDoor>(true);

        if (doors.Length <= 1) return;

        int doorsToActivate = Random.Range(1, Mathf.Min(doors.Length + 1, 4));

        //Shuffle doors
        for (int i = doors.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            DungeonDoor temp = doors[i];
            doors[i] = doors[j];
            doors[j] = temp;
        }

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].gameObject.SetActive(i < doorsToActivate);
        }
    }

    private void OnDungeonCompleted()
    {
        dungeonProgress.CompleteDungeon(dungeonData.DungeonIndex);
    }
}
