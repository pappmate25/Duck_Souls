using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] Transform playerTransform;


    private float spawnDelay = 0.3f;
    private int maxEnemy = 100;


    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        enemy.GetComponent<TrackPlayer>().Initialize(playerTransform);
    }
}
