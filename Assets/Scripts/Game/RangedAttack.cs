using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : BaseAttack
{
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject bulletPrefab;

    private AimTowardsPlayer aimTowardsPlayer;

    //bullet pool
    private int poolSize = 8;
    private Queue<GameObject> pool = new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        weaponStats = rangedWeapon.GetComponent<WeaponStats>();
        aimTowardsPlayer = GetComponentInParent<AimTowardsPlayer>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
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

        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);

        bullet.transform.position = transform.position;

        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

        bulletBehavior.ShootBullet(bulletRB, direction, weaponStats.GetProjectileSpeed());

        StartCoroutine(ReturnToPool(bullet));

        StartCooldown();
    }

    private IEnumerator ReturnToPool(GameObject bullet)
    {
        yield return new WaitForSecondsRealtime(weaponStats.GetProjectileLifeSpan());

        bullet.SetActive(false);
        pool.Enqueue(bullet);
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
