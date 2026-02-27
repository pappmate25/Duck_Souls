using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D myRigidbody;

    //actions
    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;


    //movement
    private Vector2 moveDirection;
    private float moveSpeed = 7.0f;


    //dodge
    private bool dodgeRequested;
    private float dodgeForce = 25.0f;
    private float dodgeDuration = 0.2f;
    private float dodgeCooldown = 2.0f;
    private float dodgeActiveTimer;
    private float dodgeCooldownTimer;
    private bool isDodging;
    private bool canDodge = true;
    private Vector2 dodgeDirection;


    //attack
    private bool attackRequested;
    public static Action StartAttack;
    [SerializeField] private GameObject bullet;





    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myRigidbody = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions["Move"];
        dodgeAction = playerInput.actions["Dodge"];
        attackAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        dodgeAction.performed += OnDodge;

        attackAction.performed += OnAttack;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        dodgeAction.performed -= OnDodge;

        attackAction.performed -= OnAttack;
        attackAction.canceled -= OnAttack;
    }

    private void Update()
    {
        HandleTimers();
    }

    private void FixedUpdate()
    {
        //if (alive){}
        HandleMovement();
        HandleDodge();
        HandleAttack();
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        if (isDodging) return;

        moveDirection = context.ReadValue<Vector2>();
    }

    private void OnDodge(InputAction.CallbackContext context)
    {
        if (!canDodge) return;

        dodgeRequested = true;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (isDodging) return; // + "|| !isAlive"

        attackRequested = true;
    }

    private void HandleMovement()
    {
        if (isDodging) return;

        myRigidbody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        //play anim
        GetDodgeDirection();
    }

    private void GetDodgeDirection()
    {
        if (moveDirection != Vector2.zero)
        {
            dodgeDirection = moveDirection.normalized;
        }
        print(dodgeDirection);
    }

    private void HandleDodge()
    {
        if (dodgeRequested && canDodge)
        {
            StartDodge();
        }

        dodgeRequested = false;
    }

    private void StartDodge()
    {
        isDodging = true;
        canDodge = false;
        dodgeActiveTimer = dodgeDuration;
        dodgeCooldownTimer = dodgeCooldown;

        myRigidbody.linearVelocity = dodgeDirection * dodgeForce;
        //play anim
    }

    private void HandleTimers()
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

    private void HandleAttack()
    {
        if (attackRequested)
        {
            StartAttack?.Invoke();
        }
        attackRequested = false;
    }
}
