using UnityEngine;
using UnityEngine.UIElements;

public class DamagePopup : MonoBehaviour
{
    private VisualElement root;
    private Label damageLabel;
    float lifeTime = 1f;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        damageLabel = root.Q<Label>("damage-number-label");
    }

    public void SetupDamage(int damage)
    {
        damageLabel.text = $"{damage}";
    }

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        damageLabel.style.opacity = lifeTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            lifeTime = 1f;
        }
    }
}
