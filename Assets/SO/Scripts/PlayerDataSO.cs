using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SO/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public int Hp;
    public int Level;
    public int Exp;
}
