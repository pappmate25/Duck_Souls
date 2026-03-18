using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected Weapon weapon;

    protected bool canAttack = true;
    protected float waitForNextAttack;

    protected PlayerController playerController;

    //enemy
    [Header("Enemy")]
    [SerializeField] protected bool isAI = false;
    protected bool startAttack = false;


    protected virtual void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    protected virtual void OnEnable()
    {
        if (playerController != null)
        {
            playerController.StartAttack += HandlePlayerAttack;
        }
    }

    protected virtual void OnDisable()
    {
        if (playerController != null)
        {
            playerController.StartAttack -= HandlePlayerAttack;
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
