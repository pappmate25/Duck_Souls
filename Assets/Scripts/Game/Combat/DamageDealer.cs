using System;
using Unity.Cinemachine;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private DamagePopup Manager;

    private WeaponStats weaponStats;

    public static Action OnPlayerHit;
    public static Action<Vector3, int> OnEnemyHit;


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
                OnEnemyHit?.Invoke(transform.position + Vector3.up * 2f, weaponStats.GetDamage());
            }

            if (other.gameObject.layer == playerLayerIndex)
            {
                OnPlayerHit?.Invoke();
            }
        }
    }
}
