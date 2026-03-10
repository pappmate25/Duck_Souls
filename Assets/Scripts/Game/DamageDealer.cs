using System;
using Unity.Cinemachine;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private GameObject damagePopup;
    private WeaponStats weaponStats;

    public static Action OnPlayerHit;


    private void Awake()
    {
        weaponStats = GetComponent<WeaponStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        int playerLayerIndex = LayerMask.NameToLayer("Player");

        if (health != null)
        {
            health.TakeDamage(weaponStats.GetDamage());

            if(other.gameObject.layer == enemyLayerIndex)
            {
                GameObject popup = Instantiate(damagePopup, transform.position + Vector3.up * 2f, Quaternion.identity);

                popup.GetComponent<DamagePopup>().SetupDamage(weaponStats.GetDamage());
            }

            if (other.gameObject.layer == playerLayerIndex)
            {
                OnPlayerHit?.Invoke();
            }
        }
    }
}
