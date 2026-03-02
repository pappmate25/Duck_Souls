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


    //attack
    [SerializeField] private Transform aimDirection;
    private Vector3 playerFacingDirection;
    private Camera mainCamera;
    private bool attackRequested;
    public static Action StartAttack;






    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myRigidbody = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;

        moveAction = playerInput.actions["Move"];
        dodgeAction = playerInput.actions["Dodge"];
        attackAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        dodgeAction.performed += OnDodge;

        attackAction.performed += OnAttackStart;
        attackAction.canceled += OnAttackCancel;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        dodgeAction.performed -= OnDodge;

        attackAction.performed -= OnAttackStart;
        attackAction.canceled -= OnAttackCancel;
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

    private void OnAttackStart(InputAction.CallbackContext context)
    {
        if (isDodging) return; // + "|| !isAlive"

        attackRequested = true;
    }

    private void OnAttackCancel(InputAction.CallbackContext context)
    {
        if (isDodging) return;

        attackRequested = false;
    }

    private void RotatePlayerAim()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;

        if (moveDirection != Vector2.zero) //player moves
        {
            playerFacingDirection = moveDirection.normalized; 
        }

        aimDirection.rotation = Quaternion.LookRotation(Vector3.forward, direction * -1);
    }

    private void HandleMovement()
    {
        if (isDodging) return;

        myRigidbody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        RotatePlayerAim();
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

        myRigidbody.linearVelocity = playerFacingDirection * dodgeForce;
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
    }
}
