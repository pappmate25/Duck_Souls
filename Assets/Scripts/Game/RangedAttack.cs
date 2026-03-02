using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject bulletPrefab;

    private WeaponStats weaponStats;

    private bool canAttack = true;
    private float waitForNextAttack;

    [Header("Enemy")]
    [SerializeField] private bool isAI = false;

    private void Awake()
    {
        weaponStats = rangedWeapon.GetComponent<WeaponStats>();

        PlayerController.StartAttack += StartAttack;
    }

    private void Update()
    {
        HandleTimers();
    }

    private void StartAttack(Vector2 direction, bool isRanged)
    {
        if (isAI) { return; }

        if (canAttack && isRanged)
        {
            rangedWeapon.SetActive(true);
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
            rangedWeapon.SetActive(false);
        }        
    }

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
}
