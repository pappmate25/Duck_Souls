using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDataSO", menuName = "SO/DungeonData")]
public class DungeonDataSO : ScriptableObject
{
    [SerializeField] private RoomData[] rooms;

    public RoomData[] Rooms => rooms;

}
