using System;
using Unity.Cinemachine;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private DamagePopup Manager;
    [SerializeField] private GameEventSO onPlayerHit;
    [SerializeField] private GameEventVector3IntSO onEnemyHit;

    private Weapon weapon;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        int playerLayerIndex = LayerMask.NameToLayer("Player");

        if (health != null)
        {
            health.TakeDamage(weapon.Data.Damage);

            if(other.gameObject.layer == enemyLayerIndex)
            {
                onEnemyHit?.Invoke((transform.position + Vector3.up * 2f, weapon.Data.Damage));
            }

            if (other.gameObject.layer == playerLayerIndex)
            {
                onPlayerHit?.Invoke();
                print("alma");
            }
        }
    }
}
