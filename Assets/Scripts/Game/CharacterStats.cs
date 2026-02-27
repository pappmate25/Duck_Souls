using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    private int HP => playerData.Hp;
    private int level => playerData.Level;
    private int exp => playerData.Exp;

    public int GetHp() { return HP; }
}
