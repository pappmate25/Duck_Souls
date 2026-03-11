using UnityEngine;

public class RangedAttack : BaseAttack
{
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject bulletPrefab;

    private AimTowardsPlayer aimTowardsPlayer;

    protected override void Awake()
    {
        base.Awake();

        weaponStats = rangedWeapon.GetComponent<WeaponStats>();
        aimTowardsPlayer = GetComponentInParent<AimTowardsPlayer>();       
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnEnable();
    }

    protected override void HandlePlayerAttack(Vector2 direction, bool isRanged)
    {
        if (isAI || !isRanged || !canAttack) return;

        Shoot(direction);
    }

    protected override void AttackAI()
    {
        if(!canAttack) return;

        Shoot(aimTowardsPlayer.GetAimDirection() * -1);
    }

    private void Shoot(Vector2 direction)
    {
        rangedWeapon.SetActive(true);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

        bulletBehavior.ShootBullet(bulletRB, direction, weaponStats.GetProjectileSpeed());

        Destroy(bullet, weaponStats.GetProjectileLifeSpan());

        StartCooldown();
    }

   
    //Check if the player is in attack range
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
}
