using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private DamagePopup popupPrefab;
    private int poolSize = 32;

    private Queue<DamagePopup> pool = new Queue<DamagePopup>();

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            DamagePopup popup = Instantiate(popupPrefab, transform);
            popup.gameObject.SetActive(false);
            pool.Enqueue(popup);
        }
    }

    private void OnEnable()
    {
        DamageDealer.OnEnemyHit += ShowPopup;
    }

    private void OnDisable()
    {
        DamageDealer.OnEnemyHit -= ShowPopup;
    }

    public void ShowPopup(Vector3 position, int damage)
    {
        DamagePopup popup = pool.Dequeue();

        popup.transform.position = position;
        popup.gameObject.SetActive(true);

        popup.SetupDamage(damage, ReturnToPool);
    }

    private void ReturnToPool(DamagePopup popup)
    {
        popup.gameObject.SetActive(false);
        pool.Enqueue(popup);
    }
}
