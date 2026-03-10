using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject bulletPrefab;

    private WeaponStats weaponStats;

    private bool canAttack = true;
    private float waitForNextAttack;

    private PlayerController playerController;

    [Header("Enemy")]
    [SerializeField] private bool isAI = false;
    private bool startAttack = false;
    private AimTowardsPlayer aimTowardsPlayer;

    private void Awake()
    {
        weaponStats = rangedWeapon.GetComponent<WeaponStats>();

        aimTowardsPlayer = GetComponentInParent<AimTowardsPlayer>();

        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        if (playerController != null)
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

        if (isAI && startAttack)
        {
            AttackPlayer();
        }
    }

    #region Player
    private void StartAttack(Vector2 direction, bool isRanged)
    {
        if (isAI) { return; }

        if (canAttack && isRanged)
        {
            rangedWeapon.SetActive(canAttack);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

            bulletBehavior.ShootBullet(bulletRigidbody, direction, weaponStats.GetProjectileSpeed());


            Destroy(bullet, weaponStats.GetProjectileLifeSpan());

            canAttack = false;
            waitForNextAttack = weaponStats.GetAttackRate();
        }
        else
        {
            rangedWeapon.SetActive(canAttack);
        }        
    }
    #endregion

    #region Enemy
    private void AttackPlayer()
    {
        if (canAttack)
        {
            rangedWeapon.SetActive(canAttack);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

            bulletBehavior.ShootBullet(bulletRigidbody, aimTowardsPlayer.GetAimDirection() * -1, weaponStats.GetProjectileSpeed());


            Destroy(bullet, weaponStats.GetProjectileLifeSpan());

            canAttack = false;
            waitForNextAttack = weaponStats.GetAttackRate();
        }
        else
        {
            rangedWeapon.SetActive(canAttack);
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

    private void HandleTimers()
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
    #endregion
}
