using UnityEngine;
using UnityEngine.UIElements;

public class HPBarUIManager : MonoBehaviour
{
    private VisualElement root;
    private ProgressBar healthBar;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        healthBar = root.Q<ProgressBar>("hp-bar");


        healthBar.value = healthBar.highValue;
        Health.OnHpChange += UpdateHpBar;
    }

    private void UpdateHpBar(int currentHealth)
    {
        healthBar.value = currentHealth;
    }


}
