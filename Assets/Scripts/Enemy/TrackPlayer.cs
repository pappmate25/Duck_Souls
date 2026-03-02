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

    private void Update()
    {
        FollowingPlayer();
    }

    public void InitializePlayerPosition(Transform player)
    {
        playerTransform = player;
    }

    private void FollowingPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, characterStats.GetMoveSpeed() * Time.deltaTime);
        enemyRigidbody.linearVelocity = Vector2.zero; // hogy ne lõjje ki a enemy-t a player mikor nekimegy
    }
}
