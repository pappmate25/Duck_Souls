using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonDataSO dungeonData;

    private int currentRoomIndex;
    private GameObject currentRoomInstance;


    private void Start()
    {
        StartDungeon();
    }
    //private void OnEnable()
    //{
    //    DungeonSelectUI.OnDungeonSelect += StartDungeon;
    //}

    //private void OnDisable()
    //{
    //    DungeonSelectUI.OnDungeonSelect -= StartDungeon;
    //}

    public void StartDungeon()
    {
        currentRoomIndex = 0;
        LoadRoom(currentRoomIndex);
    }

    public void LoadRoom(int index)
    {
        if (currentRoomInstance != null)
        {
            //currentRoomInstance.SetActive(true);
        }

        currentRoomIndex = index;

        RoomData room = dungeonData.Rooms[index];
        currentRoomInstance = room.RoomPrefab;
        Instantiate(currentRoomInstance);
    }

    public void LoadNextRoom()
    {
        int[] connections = dungeonData.Rooms[currentRoomIndex].ConnectedRooms;

        if (connections.Length > 0)
        {
            LoadRoom(connections[0]);
        }
    }

    public bool IsBoosRoom()
    {
        return dungeonData.Rooms[currentRoomIndex].RoomType == RoomType.Boss;
    }
}
