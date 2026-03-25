using UnityEngine;
using UnityEngine.InputSystem;

public class AimController : MonoBehaviour
{
    [SerializeField] private Transform aimPivot;

    private Camera mainCamera;

    public Vector2 AttackDirection { get; private set; }
    public Vector3 PlayerFacingDirection { get; private set; }


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void UpdateAim(Vector2 moveDirection)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        AttackDirection = (mouseWorldPos - (Vector2)transform.position).normalized;

        if (moveDirection != Vector2.zero) //if player moves
        {
            PlayerFacingDirection = moveDirection.normalized;
        }

        aimPivot.rotation = Quaternion.LookRotation(Vector3.forward, AttackDirection * -1);
    }

}
