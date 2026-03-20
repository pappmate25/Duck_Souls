using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameEventIntSO onHpChange;
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
        onHpChange.Invoke(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetMaxHealth()
    {
        return health;
    }
}
