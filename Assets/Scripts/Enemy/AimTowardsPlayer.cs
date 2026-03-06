using UnityEngine;

public class AimTowardsPlayer : MonoBehaviour
{
    [SerializeField] private Transform aimDirection;
    private Transform playerTransform;

    private void Update()
    {
        EnemyAimDirection();
    }

    public void InitializePlayerTransform(Transform player)
    {
        playerTransform = player;
    }

    private void EnemyAimDirection()
    {
        aimDirection.rotation = Quaternion.LookRotation(Vector3.forward, GetAimDirection());
    }

    public Vector3 GetAimDirection()
    {
        return ((playerTransform.position - transform.position).normalized * -1);
    }
}
