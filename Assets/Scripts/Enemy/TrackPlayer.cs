using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, characterStats.GetMoveSpeed() * Time.deltaTime);
    }

    public void Initialize(Transform player)
    {
        playerTransform = player;
    }
}
