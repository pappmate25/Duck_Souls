using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 7.0f;

    //dodge
    private float dodgeForce = 25.0f;
    private float dodgeDuration = 0.2f;
    private float dodgeCooldown = 2.0f;
    private float dodgeActiveTimer;
    private float dodgeCooldownTimer;
    private bool isDodging;
    private bool canDodge = true;
    private Vector2 dodgeDirection;



    private PlayerInput playerInput;
    private Rigidbody2D myRigidbody;

    private InputAction moveAction;
    private InputAction dodgeAction;


    private Vector2 moveDirection;
    private bool dodgeRequested;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myRigidbody = GetComponent<Rigidbody2D>();

        moveAction = playerInput.actions["Move"];
        dodgeAction = playerInput.actions["Dodge"];
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        dodgeAction.performed += OnDodge;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        dodgeAction.performed -= OnDodge;
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

    private void HandleMovement()
    {
        if (isDodging) return;

        myRigidbody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
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
}
