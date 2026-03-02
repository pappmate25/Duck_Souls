using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public void ShootBullet(Rigidbody2D bulletRigidbody, Vector2 direction, float speed)
    {
        bulletRigidbody.AddForce(direction * 40, ForceMode2D.Impulse);
    }
}
