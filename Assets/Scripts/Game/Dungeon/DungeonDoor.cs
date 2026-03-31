using UnityEngine;

public enum DoorType
{
    Forward,
    Back
}

public enum WallSide
{
    Top,
    Bottom,
    Left,
    Right
}



public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private DoorType doorType;
    [SerializeField] private WallSide wallSide;
    [SerializeField] private GameEventSO RoomManager_onRoomCleared;
    [SerializeField] private SpriteRenderer doorVisuals;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;
    [SerializeField] private Color unlockedBossDoorColor = Color.black;

    private DungeonManager dungeonManager;
    private int targetRoomIndex;
    private bool isUnlocked;

    public DoorType DoorType => doorType;
    public WallSide WallSide => wallSide;

    private void Awake()
    {
        dungeonManager = FindFirstObjectByType<DungeonManager>();
    }

    private void OnEnable()
    {
        isUnlocked = false;
        UpdateVisual();
        RoomManager_onRoomCleared.Subscribe(Unlock);
    }

    private void OnDisable()
    {
        RoomManager_onRoomCleared.UnSubscribe(Unlock);
    }

    public void SetTarget(int roomIndex)
    {
        targetRoomIndex = roomIndex;
    }

    public void SetAsBossDoor()
    {
        unlockedColor = unlockedBossDoorColor;
        UpdateVisual();
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if(doorVisuals != null)
        {
            doorVisuals.color = isUnlocked ? unlockedColor : lockedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;

        int layerIndex = LayerMask.NameToLayer("Player");
        if(other.gameObject.layer == layerIndex)
        {
            // Tell DungeonManager which wall the player is exiting through
            dungeonManager.LoadRoom(targetRoomIndex, wallSide);
        }
    }
}
