using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
    private WeaponStats weaponStats;

    private bool canAttack = true;
    private float waitForNextAttack;

    [Header("Enemy")]
    [SerializeField] private bool isAI = false;

    private void Awake()
    {
        weaponStats = weaponPrefab.GetComponent<WeaponStats>();

        PlayerController.StartAttack += StartAttack;
    }

    private void Update()
    {
        HandleTimers();
    }

    private void StartAttack()
    {        
        if (canAttack && !isAI)
        {
            waitForNextAttack = weaponStats.GetAttackRate();

            GameObject clone = Instantiate(weaponPrefab, transform.position, Quaternion.identity);

            //play anim

            Destroy(clone, weaponStats.GetProjectileLifeSpan());

            canAttack = false;
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
