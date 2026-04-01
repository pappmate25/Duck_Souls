using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private WaveSO[] waveSO;

    private WaveSO currentWave;
    private float delayBetweenWaves = 5f;
    private Transform playerTransform;
    private RoomManager roomManager;


    private void Start()
    {
        playerTransform = FindFirstObjectByType<PlayerController>().transform;
        roomManager = GetComponent<RoomManager>();
        StartNextWave();
    }

    private void StartNextWave()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        foreach (WaveSO wave in waveSO)
        {
            currentWave = wave;

            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(i), GetRandomSpawnPosition(playerTransform), Quaternion.identity);

                enemy.GetComponent<TrackPlayer>().InitializePlayerPosition(playerTransform);
                enemy.GetComponent<AimTowardsPlayer>().InitializePlayerTransform(playerTransform);

                if (roomManager != null)
                {
                    roomManager.RegisterEnemy();
                }

                yield return new WaitForSeconds(currentWave.GetSpawnDelay());
            }
            yield return new WaitForSeconds(delayBetweenWaves);
        }
    }

    private Vector2 GetRandomSpawnPosition(Transform playerLocation)
    {
        float innerRadius = 8f; //where enemies are not able to spawn
        float outerRadius = 20f; //where enemies are able to spawn


        float randomRadius = Mathf.Sqrt(Random.Range(innerRadius * innerRadius, outerRadius * outerRadius));
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector2 spawnPoint = randomDirection * randomRadius;
        
        //return  (Vector2)playerLocation.position + spawnPoint;
        return  Vector2.zero + spawnPoint;
    }
}
