using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Character character;
    private int health;
    public Action<int> OnHpChange;


    private void Awake()
    {
        character = GetComponent<Character>();
        health = character.Data.HP;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHpChange?.Invoke(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
