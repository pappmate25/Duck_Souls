using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private CharacterDataSO characterData;
    private int HP => characterData.Hp;
    private int level => characterData.Level;
    private int exp => characterData.Exp;
    private float moveSpeed => characterData.MoveSpeed;
    private WeaponType weaponTpye => characterData.WeaponType;


    public int GetHp() { return HP; }
    public int GetLevel() { return level; }
    public int GetExp() { return exp; }
    public float GetMoveSpeed() {  return moveSpeed; }
    public WeaponType GetWeaponTpye() {  return weaponTpye; }
}
