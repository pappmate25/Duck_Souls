using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private CharacterStats characterStats;
    private Rigidbody2D enemyRigidbody;


    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FollowingPlayer();
    }

    public void InitializePlayerPosition(Transform player)
    {
        playerTransform = player;
    }

    private void FollowingPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized; //irányvektor

        enemyRigidbody.linearVelocity = direction * characterStats.GetMoveSpeed();
        //enemyRigidbody.linearVelocity = Vector2.zero; // hogy ne lõjje ki a enemy-t a player mikor nekimegy
    }
}
