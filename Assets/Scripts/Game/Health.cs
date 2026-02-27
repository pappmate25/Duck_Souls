using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private CharacterStats characterStats;
    private int health;
    public static Action<int> OnHpChange;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        health = characterStats.GetHp();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHpChange?.Invoke(health);
    }
}
