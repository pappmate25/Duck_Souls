using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private Stack<VisualElement> uiStack = new Stack<VisualElement>();
    
    public void OpenUI(VisualElement root, bool pauseGame)
    {
        root.style.display = DisplayStyle.Flex;
        print(root);
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
        //So if pause if open uiStack.Count == 1
        //On close pause pops --> uiStack.Count == 0 --> Time.timeScale = 1f;
        if (uiStack.Count == 0)
        {
            Time.timeScale = 1f;
        }
    }

    public bool HasOpenUI()
    {
        return uiStack.Count > 0;
    }
}
