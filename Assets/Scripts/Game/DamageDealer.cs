using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private GameObject damagePopup;
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
        int layerIndex = LayerMask.NameToLayer("Enemy");

        if (health != null)
        {
            health.TakeDamage(damage);

            if(other.gameObject.layer == layerIndex)
            {
                GameObject popup = Instantiate(damagePopup, transform.position + Vector3.up * 2f, Quaternion.identity);

                popup.GetComponent<DamagePopup>().SetupDamage(damage);
            }
        }
    }
}
