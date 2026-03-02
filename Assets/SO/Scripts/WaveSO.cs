using UnityEngine;

[CreateAssetMenu(fileName ="Wave", menuName = "SO/Wave")]
public class WaveSO : ScriptableObject
{
    [SerializeField] private GameObject[] enemyPrefabs;
    public float delayBetweenEnemySpawn;


    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Length;
    }

    public float GetSpawnDelay()
    {
        return delayBetweenEnemySpawn;
    }
}
