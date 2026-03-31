using UnityEngine;
using UnityEngine.UIElements;

public class WeaponchoiceUI : MonoBehaviour
{
    [SerializeField] private GameEventBoolSO WeaponchoiceUI_onShowUI;

    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        WeaponchoiceUI_onShowUI.Subscribe(ToggleUI);
    }

    private void OnDisable()
    {
        WeaponchoiceUI_onShowUI.UnSubscribe(ToggleUI);
    }

    private void ToggleUI(bool activate)
    {
        root.style.display = activate ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
