using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D myRigidbody;
    private Character character;

    //actions
    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;
    private InputAction interactAction;


    //movement
    private Vector2 moveDirection;


    //dodge
    private bool dodgeRequested;
    private DodgeController dodgeController;


    //attack
    [SerializeField] private GameEventVector2BoolSO onStartAttack;

    private AimController aimController;
    private bool attackRequested;
    private bool isRanged;


    //interact
    private PlayerInteraction playerInteraction;
    private bool interactRequested;



    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        myRigidbody = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        playerInteraction = GetComponent<PlayerInteraction>();
        dodgeController = GetComponent<DodgeController>();
        aimController = GetComponent<AimController>();


        moveAction = playerInput.actions["Move"];
        dodgeAction = playerInput.actions["Dodge"];
        attackAction = playerInput.actions["Attack"];
        interactAction = playerInput.actions["Interact"];

        SetRangedOrMelee(character.Data.WeaponType);
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        dodgeAction.performed += OnDodge;

        attackAction.performed += OnAttackStart;
        attackAction.canceled += OnAttackCancel;

        interactAction.performed += OnInteract;
    }
    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        dodgeAction.performed -= OnDodge;

        attackAction.performed -= OnAttackStart;
        attackAction.canceled -= OnAttackCancel;

        interactAction.performed -= OnInteract;
    }

    private void FixedUpdate()
    {
        //if (alive){}
        if (!dodgeController.IsDodging)
        {
            HandleMovement();

            if(attackRequested)
                HandleAttack();
        }

        if(dodgeRequested)
            HandleDodge();

        if (interactRequested)
            HandleInteraction();
    }


    private void OnMove(InputAction.CallbackContext context) => moveDirection = context.ReadValue<Vector2>();

    private void OnDodge(InputAction.CallbackContext context) => dodgeRequested = true;

    private void OnAttackStart(InputAction.CallbackContext context) => attackRequested = true;

    private void OnAttackCancel(InputAction.CallbackContext context) => attackRequested = false;

    private void OnInteract(InputAction.CallbackContext context) => interactRequested = true;

   
    private void HandleMovement()
    {
        myRigidbody.linearVelocity = new Vector2(moveDirection.x * character.Data.MoveSpeed,
                                                 moveDirection.y * character.Data.MoveSpeed);

        aimController.UpdateAim(moveDirection);
    }

    private void HandleDodge()
    {
        dodgeController.TryDodge(aimController.PlayerFacingDirection);
        dodgeRequested = false;
    }

    private void HandleAttack()
    {
        onStartAttack.Invoke((aimController.AttackDirection, isRanged));
    }

    private void HandleInteraction()
    {
        playerInteraction.Interact();
        interactRequested = false;
    }

    public void SetRangedOrMelee(WeaponType weaponType)
    {
        isRanged = weaponType == WeaponType.Ranged;
    }
}
