using UnityEngine;

[CreateAssetMenu(fileName ="SceneData", menuName ="SO/SceneData")]
public class SceneDataSO : ScriptableObject
{
    [SerializeField] private string hubScene;
    [SerializeField] private string[] dungeonScenes; // index 0 = dungeon 1, etc.

    public string HubScene => hubScene;

    public string GetDungeonScene(int dungeonIndex)
    {
        int i = dungeonIndex - 1; // buttons are 1-based not 0-based

        if(i < 0 || i >= dungeonScenes.Length)
        {
            Debug.LogError($"Dungeon index {dungeonIndex} is out of range.");
            return null;
        }

        return dungeonScenes[i];
    }
}
