using UnityEngine;
using UnityEngine.UIElements;

public class HPBarUIManager : MonoBehaviour
{
    [SerializeField] private Health health;

    private VisualElement root;
    private ProgressBar healthBar;



    private void OnEnable()
    {
        health.OnHpChange += UpdateHpBar;
    }

    private void OnDisable()
    {
        health.OnHpChange +=UpdateHpBar;
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("hp-bar");

        healthBar.highValue = health.GetMaxHealth();
        healthBar.value = healthBar.highValue;
    }

    private void UpdateHpBar(int currentHealth)
    {
        healthBar.value = currentHealth;
    }
}
