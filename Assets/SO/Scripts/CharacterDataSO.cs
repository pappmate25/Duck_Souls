using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "SO/CharacterData")]
public class CharacterDataSO : ScriptableObject
{
    [SerializeField] private int hp;
    [SerializeField] private int level;
    [SerializeField] private int exp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private WeaponType weaponType;

    public int HP => hp;
    public int Level => level;
    public int Exp => exp;
    public float MoveSpeed => moveSpeed;
    public WeaponType WeaponType => weaponType;
}
