using Unity.VisualScripting;
using UnityEngine;


public enum WeaponType
{
    Ranged,
    Melee
}

[CreateAssetMenu(fileName ="WeaponData", menuName ="SO/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifespan;
    [SerializeField] private WeaponType weaponType;

    public int Damage => damage;
    public float AttackRate => attackRate;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileLifespan => projectileLifespan;
    public WeaponType WeaponType => weaponType;
}
