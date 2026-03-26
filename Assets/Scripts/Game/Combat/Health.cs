using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameEventSO deathEvent;
    [SerializeField] private bool destroyOnDeath = true;

    public Action<int> OnHpChange;

    private Character character;
    private int health;


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
            deathEvent.Invoke();

            if(destroyOnDeath) //enemy
            {
                Destroy(gameObject);
            }
        }
    }

    public int GetMaxHealth()
    {
        return character.Data.HP;
    }
}
