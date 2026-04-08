using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameEventSO enemyDeathEvent;
    [SerializeField] private GameEventSO RoomManager_onRoomCleared;
    [SerializeField] private bool isBossRoom;
    [SerializeField] private GameEventSO DungeonManager_onDungeonCompleted;

    private int enemiesAlive;
    private bool isCleared;

    private void OnEnable()
    {
        enemyDeathEvent.Subscribe(OnEnemyDeath);
    }

    private void OnDisable()
    {
        enemyDeathEvent.UnSubscribe(OnEnemyDeath);
    }

    public void RegisterEnemy()
    {
        enemiesAlive++;
    }

    public void ClearImmediately()
    {
        if(isCleared) return;
        isCleared = true;
        RoomManager_onRoomCleared.Invoke();
    }

    private void OnEnemyDeath()
    {
        enemiesAlive--;

        if(enemiesAlive <= 0 && !isCleared)
        {
            isCleared = true;
            RoomManager_onRoomCleared.Invoke();

            if (isBossRoom)
            {
                DungeonManager_onDungeonCompleted.Invoke();
                print("boss dead, dungeon completed");
            }
        }
    }
}
