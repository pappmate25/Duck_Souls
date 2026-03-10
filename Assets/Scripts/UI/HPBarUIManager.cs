using UnityEngine;
using UnityEngine.UIElements;

public class HPBarUIManager : MonoBehaviour
{
    private VisualElement root;
    private ProgressBar healthBar;
    [SerializeField] Health health;


    private void OnEnable()
    {
        health.OnHpChange += UpdateHpBar;
    }

    private void OnDisable()
    {
        health.OnHpChange -= UpdateHpBar;
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("hp-bar");


        healthBar.value = healthBar.highValue;
    }

    private void UpdateHpBar(int currentHealth)
    {
        healthBar.value = currentHealth;
    }


}
