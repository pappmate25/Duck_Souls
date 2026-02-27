using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private CharacterStats characterStats;
    private int health;
    public static Action<int> OnHpChange;


    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
        health = characterStats.GetHp();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHpChange?.Invoke(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log($"RIP {gameObject.name}");
        }
    }
}
