using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject meleeWeaponPrefab;

    private void Start()
    {
        PlayerController.StartAttack += StartAttack;
    }

    private void StartAttack()
    {
        Instantiate(meleeWeaponPrefab, transform.position, Quaternion.identity);
        //play anim
    }
}
