using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject meleeWeapon;
    [SerializeField] private GameObject rangedWeapon;
    [SerializeField] private GameObject bulletPrefab;
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

    private void StartAttack()
    {
        if (isAI) { return; }

        if (weaponStats.GetWeaponType() == WeaponType.Melee)
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

        //else
        //{
        //    if (canAttack)
        //    {
        //        rangedWeapon.SetActive(true);
        //        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //        bulletRigidbody.linearVelocity = Vector2.right * weaponStats.GetProjectileSpeed();

        //        Destroy(bullet, weaponStats.GetProjectileLifeSpan());

        //        canAttack = false;
        //        waitForNextAttack = weaponStats.GetAttackRate();
        //    }
        //    else
        //    {
        //        rangedWeapon.SetActive(false);
        //    }
        //}   
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















    //ranged later
    //if (weaponStats.GetWeaponType() == WeaponType.Ranged && canAttack)
    //{
    //    waitForNextAttack = weaponStats.GetAttackRate();

    //    GameObject clone = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
    //    //bullet instantiate --> needs bullet behavior class
    //}
    //else
    //{

    //}


}
