using UnityEngine;
using UnityEngine.UIElements;

public class SwordUI : MonoBehaviour
{
    [SerializeField] private GameEventBoolSO PlayerInteraction_onShowSwordUI;

    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        PlayerInteraction_onShowSwordUI.Subscribe(ToggleUI);
    }

    private void OnDisable()
    {
        PlayerInteraction_onShowSwordUI.Subscribe(ToggleUI);
    }

    private void ToggleUI(bool activate)
    {
        root.style.display = activate ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
