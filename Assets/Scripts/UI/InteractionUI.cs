using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionUI : MonoBehaviour
{
    private VisualElement root;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        PlayerInteraction.OnShowInteractUI += ShowUI;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnShowInteractUI -= ShowUI;
    }

    private void ShowUI(bool value)
    {
        if (value) root.style.display = DisplayStyle.Flex;
        else root.style.display = DisplayStyle.None;
    }
}
