using System.Collections;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    [SerializeField] private GameObject meleeWeapon;

    protected override void Awake()
    {
        base.Awake();

        weapon = meleeWeapon.GetComponent<Weapon>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void HandlePlayerAttack(Vector2 direction, bool isRanged)
    {
        if (isAI || isRanged || !canAttack) return;

        Attack();
    }

    protected override void AttackAI()
    {
        if(!canAttack) return;

        Attack();
    }

    private void Attack()
    {
        if (!canAttack) return;

        StartCooldown();
        StartCoroutine(MeleeRoutine());
    }

    private IEnumerator MeleeRoutine()
    {
        meleeWeapon.SetActive(true);

        yield return new WaitForSeconds(0.15f); //lower then the attackRate

        meleeWeapon.SetActive(false);
    }

    //Checks if the player is in attack range
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
