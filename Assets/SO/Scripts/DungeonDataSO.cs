using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDataSO", menuName = "SO/DungeonData")]
public class DungeonDataSO : ScriptableObject
{
    [SerializeField] private int dungeonIndex;
    [SerializeField] private bool isCompleted;
    [SerializeField] private RoomData[] rooms;


    public int DungeonIndex => dungeonIndex;
    public bool IsCompleted => isCompleted;
    public RoomData[] Rooms => rooms;


    public void SetCompletion(bool value)
    {
        isCompleted = value;
    }
}
