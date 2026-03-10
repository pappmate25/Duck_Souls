using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //both
    [SerializeField] private GameObject meleeWeapon;

    private WeaponStats weaponStats;

    private bool canAttack = true;
    private float waitForNextAttack;

    PlayerController playerController;

    //enemy
    [Header("Enemy")]
    [SerializeField] private bool isAI = false;
    private bool startAttack = false;

    private void Awake()
    {
        weaponStats = meleeWeapon.GetComponent<WeaponStats>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        if( playerController != null)
        {
            playerController.StartAttack += StartAttack;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.StartAttack -= StartAttack;
        }
    }

    private void Update()
    {
        HandleTimers();

        if(isAI && startAttack)
        {
            AttackPlayer();
        }
    }

    #region Player
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
    #endregion

    #region Enemy
    //enemy
    private void AttackPlayer()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (other.gameObject.layer == layerIndex)
        {
            startAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (other.gameObject.layer == layerIndex)
        {
            startAttack = false;
        }
    }
    #endregion


    #region Timers
    //both
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
    #endregion
}
