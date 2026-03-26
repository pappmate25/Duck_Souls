using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private int targetRoomIndex;
    [SerializeField] private GameEventSO RoomClearedEvent;
    [SerializeField] private SpriteRenderer doorVisuals;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;

    private DungeonManager dungeonManager;
    private bool isUnlocked;

    private void Awake()
    {
        dungeonManager = FindFirstObjectByType<DungeonManager>();
    }

    private void OnEnable()
    {
        isUnlocked = false;
        UpdateVisual();
        RoomClearedEvent.Subscribe(Unlock);
    }

    private void OnDisable()
    {
        RoomClearedEvent.UnSubscribe(Unlock);
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
            doorVisuals.color = isUnlocked ? lockedColor : unlockedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;

        int layerIndex = LayerMask.NameToLayer("Player");
        if(other.gameObject.layer == layerIndex)
        {
            dungeonManager.LoadRoom(targetRoomIndex);
        }
    }
}
