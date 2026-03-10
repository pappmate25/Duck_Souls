using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DamagePopup : MonoBehaviour
{
    private VisualElement root;
    private Label damageLabel;

    public void SetupDamage(int damage, Action<DamagePopup> onComplete)
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        damageLabel = root.Q<Label>("damage-number-label");

        StopAllCoroutines();

        damageLabel.text = damage.ToString();

        StartCoroutine(PlayAnimation(onComplete));
    }

    private IEnumerator PlayAnimation(Action<DamagePopup> onComplete)
    {
        float lifeTime = 1f;
        float timer = 0f;

        while (timer < lifeTime)
        {
            timer += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime;
            damageLabel.style.opacity = 1 - timer;
            yield return null;
        }

        onComplete?.Invoke(this);
    }
}
