using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    private CharacterStats characterStats;

    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, characterStats.GetMoveSpeed() * Time.deltaTime);
    }
}
