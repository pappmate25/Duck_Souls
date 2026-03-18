using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Character character;
    private Rigidbody2D enemyRigidbody;


    private void Start()
    {
        character = GetComponent<Character>();
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

        enemyRigidbody.linearVelocity = direction * character.Data.MoveSpeed;
    }
}
