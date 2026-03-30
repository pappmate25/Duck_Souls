using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpearUI : MonoBehaviour
{
    [SerializeField] private GameEventBoolSO PlayerInteraction_onShowSpearUI;

    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        PlayerInteraction_onShowSpearUI.Subscribe(ToggleUI);
    }

    private void OnDisable()
    {
        PlayerInteraction_onShowSpearUI.Subscribe(ToggleUI);
    }

    private void ToggleUI(bool activate)
    {
        root.style.display = activate? DisplayStyle.Flex : DisplayStyle.None;
    }
}
