using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private WeaponStats weaponStats;
    private int damage;

    private void Awake()
    {
        weaponStats = GetComponent<WeaponStats>();
        damage = weaponStats.GetDamage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject, 0.1f);
    }
}
