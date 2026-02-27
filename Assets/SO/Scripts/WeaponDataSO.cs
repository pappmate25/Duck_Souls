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
    public int Damage;
    public float AttackRate;
    public float ProjectileSpeed;
    public float ProjectileLifeSpan;
    public WeaponType WeaponType;
}
