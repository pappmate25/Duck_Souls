using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    [SerializeField] private DamagePopup popupPrefab;
    [SerializeField] private GameEventVector3IntSO onEnemyHit;

    private int poolSize = 64;

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
        onEnemyHit.Subscribe(data => ShowPopup(data.Item1, data.Item2));
    }

    private void OnDisable()
    {
        onEnemyHit.UnSubscribe(data => ShowPopup(data.Item1, data.Item2));
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
