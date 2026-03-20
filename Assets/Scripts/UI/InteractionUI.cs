using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameEventBoolSO onShowInteractUI;
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        onShowInteractUI.Subscribe(ShowUI);
    }

    private void OnDisable()
    {
        onShowInteractUI.UnSubscribe(ShowUI);
    }

    private void ShowUI(bool value)
    {
        root.style.display = value? DisplayStyle.Flex : DisplayStyle.None;
    }
}
