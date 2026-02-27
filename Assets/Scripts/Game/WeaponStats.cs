using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    private int damage => weaponData.Damage;
    private float attackRate => weaponData.AttackRate;
    private float projectileSpeed => weaponData.ProjectileSpeed;
    private float projectileLifeSpan => weaponData.ProjectileLifeSpan;
    private WeaponType weaponType => weaponData.WeaponType;

    public int GetDamage() { return damage; }

}
