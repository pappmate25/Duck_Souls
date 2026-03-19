using System.Runtime.CompilerServices;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private int targetRoomIndex;

    private DungeonManager dungeonManager;

    private void Awake()
    {
        dungeonManager = FindFirstObjectByType<DungeonManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (room != cleared) return;

        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex)
        {
            dungeonManager.LoadRoom(targetRoomIndex);
        }
    }
}
