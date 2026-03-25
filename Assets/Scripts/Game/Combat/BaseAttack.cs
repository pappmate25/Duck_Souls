using System;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected Weapon weapon;

    protected bool canAttack = true;
    protected float waitForNextAttack;

    protected PlayerController playerController;
    [SerializeField] protected GameEventVector2BoolSO PlayerController_onStartAttack;

    private Action<(Vector2, bool)> attackHandler;

    //enemy
    [Header("Enemy")]
    [SerializeField] protected bool isAI = false;
    protected bool startAttack = false;


    protected virtual void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        attackHandler = data => HandlePlayerAttack(data.Item1, data.Item2);
    }

    protected virtual void OnEnable()
    {
        if (playerController != null)
        {
            PlayerController_onStartAttack.Subscribe(attackHandler);
        }
    }

    protected virtual void OnDisable()
    {
        if (playerController != null)
        {
            PlayerController_onStartAttack.UnSubscribe(attackHandler);
        }
    }

    protected virtual void Update()
    {
        HandleTimers();

        if (isAI && startAttack)
        {
            AttackAI();
        }
    }

    protected void HandleTimers()
    {
        if (!canAttack)
        {
            waitForNextAttack -= Time.deltaTime;
            if (waitForNextAttack <= 0)
            {
                canAttack = true;
            }
        }
    }

    protected abstract void HandlePlayerAttack(Vector2 direction, bool isRanged);
    protected abstract void AttackAI();

    protected void StartCooldown()
    {
        canAttack = false;
        waitForNextAttack = weapon.Data.AttackRate;
    }
}
