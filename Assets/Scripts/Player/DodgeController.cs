using UnityEngine;

public class DodgeController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private float dodgeForce = 25.0f;
    private float dodgeDuration = 0.3f;
    private float dodgeCooldown = 0.2f;
    private float dodgeActiveTimer;
    private float dodgeCooldownTimer;
    private bool isDodging;
    private bool canDodge = true;

    public bool IsDodging => isDodging;


    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDodging)
        {
            dodgeActiveTimer -= Time.deltaTime;
            if (dodgeActiveTimer <= 0)
            {
                isDodging = false;
            }
        }

        if (!canDodge && !isDodging)
        {
            dodgeCooldownTimer -= Time.deltaTime;
            if (dodgeCooldownTimer <= 0)
            {
                canDodge = true;
            }
        }
    }

    public void TryDodge(Vector3 facingDirection)
    {
        if (!canDodge) return;

        isDodging = true;
        canDodge = false;
        dodgeActiveTimer = dodgeDuration;
        dodgeCooldownTimer = dodgeCooldown;

        myRigidbody.linearVelocity = facingDirection * dodgeForce;
    }
}
