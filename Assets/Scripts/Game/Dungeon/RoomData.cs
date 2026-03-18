using UnityEngine;


public enum RoomType
{
    Start,
    Normal,
    Boss
}

[System.Serializable]
public class RoomData : MonoBehaviour
{
    [SerializeField] private RoomType roomType;
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private int[] connectedRooms;
}
