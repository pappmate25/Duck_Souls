using UnityEngine;


public enum RoomType
{
    Start,
    Normal,
    Boss
}

[System.Serializable]
public class RoomData
{
    [SerializeField] private RoomType roomType;
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private int[] connectedRooms;

    public RoomType RoomType => roomType;
    public GameObject RoomPrefab => roomPrefab;
    public int[] ConnectedRooms => connectedRooms;
}
