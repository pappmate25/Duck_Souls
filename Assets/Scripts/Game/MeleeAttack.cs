using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject meleeWeapon;

    private WeaponStats weaponStats;

    private bool canAttack = true;
    private float waitForNextAttack;

    [Header("Enemy")]
    [SerializeField] private bool isAI = false;

    private void Awake()
    {
        weaponStats = meleeWeapon.GetComponent<WeaponStats>();

        PlayerController.StartAttack += StartAttack;
    }

    private void Update()
    {
        HandleTimers();
    }

    private void StartAttack(Vector2 direction, bool isRanged)
    {
        if (isAI || isRanged) { return; }

        if (canAttack)
        {
            meleeWeapon.SetActive(canAttack);
            canAttack = false;
            waitForNextAttack = weaponStats.GetAttackRate();
        }
        else
        {
            meleeWeapon.SetActive(canAttack);
        }      
    }

    private void HandleTimers()
    {
        if (!canAttack)
        {
            waitForNextAttack -= Time.deltaTime;
            if(waitForNextAttack <= 0)
            {
                canAttack = true;
            }
        }
    }
}
