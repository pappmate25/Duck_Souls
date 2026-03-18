using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonDataSO dungeonData;

    private int currentRoomIndex;
    private GameObject currentRoomInstance;

    public void StartDungeon()
    {
        currentRoomIndex = 0;
        LoadRoom(currentRoomIndex);
    }

    public void LoadRoom(int index)
    {
        if (currentRoomInstance != null)
        {
            Destroy(currentRoomInstance);
        }

        currentRoomIndex = index;

        RoomData room = dungeonData.Rooms[index];
    }
}
