using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform playerTransform;
    //[SerializeField] private WaveDataSO waveDataSO;


    private void Start()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(playerTransform), Quaternion.identity);

            enemy.GetComponent<TrackPlayer>().InitializePlayerPosition(playerTransform); //set the new clone to track the player
        }
    }

    private Vector2 GetRandomSpawnPosition(Transform playerLocation)
    {
        float innerRadius = 8f; //where enemies are not able to spawn
        float outerRadius = 20f; //where enemies are able to spawn


        float randomRadius = Mathf.Sqrt(Random.Range(innerRadius * innerRadius, outerRadius * outerRadius));
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector2 spawnPoint = randomDirection * randomRadius;
        
        return  (Vector2)playerLocation.position + spawnPoint;
    }
}
