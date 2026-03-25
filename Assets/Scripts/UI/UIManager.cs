using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameEventSO UIManager_onPauseClosed;

    private Stack<VisualElement> uiStack = new Stack<VisualElement>();

    
    public void OpenUI(VisualElement root, bool pauseGame)
    {
        root.style.display = DisplayStyle.Flex;
        uiStack.Push(root);

        if (pauseGame)
        {
            Time.timeScale = 0f;
        }
    }

    public void CloseTopUI()
    {
        if (uiStack.Count == 0) return;

        VisualElement top = uiStack.Pop();
        top.style.display = DisplayStyle.None;

        //pause is not avaiable if any ui is open like dungeon select UI
        //So if pause is open uiStack.Count == 1
        //On close pause pops --> uiStack.Count == 0 --> Time.timeScale = 1f;
        if (uiStack.Count == 0)
        {
            Time.timeScale = 1f;
            UIManager_onPauseClosed.Invoke();
        }
    }

    public bool HasOpenUI()
    {
        return uiStack.Count > 0;
    }
}
