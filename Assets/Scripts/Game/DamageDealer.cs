using Unity.Cinemachine;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private GameObject damagePopup;
    private WeaponStats weaponStats;
    private int damage;
    private CinemachineCamera cmCam;
    private CameraShake cameraShake;

    private void Awake()
    {
        weaponStats = GetComponent<WeaponStats>();
        damage = weaponStats.GetDamage();

        cmCam = FindFirstObjectByType<CinemachineCamera>();
        cameraShake = cmCam.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        int playerLayerIndex = LayerMask.NameToLayer("Player");

        if (health != null)
        {
            health.TakeDamage(damage);

            if(other.gameObject.layer == enemyLayerIndex)
            {
                GameObject popup = Instantiate(damagePopup, transform.position + Vector3.up * 2f, Quaternion.identity);

                popup.GetComponent<DamagePopup>().SetupDamage(damage);
            }

            if (other.gameObject.layer == playerLayerIndex && cameraShake != null)
            {
                cameraShake.Shake();
                print("alma");
            }
        }
    }
}
