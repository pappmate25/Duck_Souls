using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    private int critMultiplier = 1;

    public WeaponDataSO Data => weaponData;

    public int CritDamage => weaponData.Damage * critMultiplier;

    public void AddCritMultiplier(int value)
    {
        critMultiplier += value;
    }
}
