using Unity.VisualScripting;
using UnityEngine;

public class HandleCooldowns : MonoBehaviour
{


    private void Start()
    {
        Movement.IsDodgeActive += StartDodgeTimer;
    }

    private void StartDodgeTimer()
    {
        dodgeCooldownTimer -= Time.deltaTime;
        if (dodgeCooldownTimer <= 0)
        {
            canDodge = true;
        }
    }
    
}
