using UnityEngine;

[CreateAssetMenu(fileName = "DungeonProgress", menuName = "SO/DungeonProgress")]
public class DungeonProgressSO : ScriptableObject
{
    [SerializeField] private DungeonDataSO[] allDungeons;

    public bool IsDungeonUnlocked(int dungeonIndex)
    {
        if (dungeonIndex <= 1) return true;
        return allDungeons[dungeonIndex - 2].IsCompleted;
    }

    public void CompleteDungeon(int dungeonIndex)
    {
        int i = dungeonIndex - 1;
        if (i >= 0 && i <= allDungeons.Length)
        {
            allDungeons[i].SetCompletion(true);
            PlayerPrefs.SetInt($"Dungeon_{dungeonIndex}_Completed", 1);
            PlayerPrefs.Save();
        }
    }

    public void LoadProgress()
    {
        for (int i = 0; i < allDungeons.Length; i++)
        {
            int dungeonIndex = i + 1;
            bool completed = PlayerPrefs.GetInt($"Dungeon_{dungeonIndex}_Completed", 0) == 1;
            allDungeons[i].SetCompletion(completed);
        }
    }

    public void ResetAllProgress()
    {
        for (int i = 0; i < allDungeons.Length; i++)
        {
            allDungeons[i].SetCompletion(false);
            PlayerPrefs.DeleteKey($"Dungeon_{i + 1}_Completed");
        }
        PlayerPrefs.Save();
    }
}
