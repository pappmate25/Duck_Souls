using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonSelectUI : MonoBehaviour
{
    private VisualElement root;

    //Dungeons
    private Button dungeonButton1;
    private Button dungeonButton2;
    private Button dungeonButton3;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private DungeonProgressSO dungeonProgress;

    [Header("Events")]
    [SerializeField] private GameEventIntSO DungeonSelectUI_onDungeonSelect;
    [SerializeField] private GameEventSO NPC_onInteraction;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        dungeonButton1 = root.Q<Button>("dungeon-button-1");
        dungeonButton2 = root.Q<Button>("dungeon-button-2");
        dungeonButton3 = root.Q<Button>("dungeon-button-3");

        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        NPC_onInteraction.Subscribe(ActivateDungeonUI);

        dungeonButton1.clicked += Dungeon1;
        dungeonButton2.clicked += Dungeon2;
        dungeonButton3.clicked += Dungeon3;

    }

    private void OnDisable()
    {
        NPC_onInteraction.UnSubscribe(ActivateDungeonUI);

        dungeonButton1.clicked -= Dungeon1;
        dungeonButton2.clicked -= Dungeon2;
        dungeonButton3.clicked -= Dungeon3;
    }

    private void ActivateDungeonUI()
    {
        UpdateButtonStates();
        uiManager.OpenUI(root, false);
    }

    private void UpdateButtonStates()
    {
        SetButtonLock(dungeonButton1, !dungeonProgress.IsDungeonUnlocked(1));
        SetButtonLock(dungeonButton2, !dungeonProgress.IsDungeonUnlocked(2));
        SetButtonLock(dungeonButton3, !dungeonProgress.IsDungeonUnlocked(3));
    }

    private void SetButtonLock(Button button, bool locked)
    {
        button.SetEnabled(!locked);
        if (locked)
        {
            button.AddToClassList("locked");
        }
        else
        {
            button.RemoveFromClassList("locked");
        }
    }


    private void Dungeon1() => DungeonSelect(1);
    private void Dungeon2() => DungeonSelect(2);
    private void Dungeon3() => DungeonSelect(3);
    private void DungeonSelect(int dungeonIndex)
    {
        if (!dungeonProgress.IsDungeonUnlocked(dungeonIndex)) return;

        DungeonSelectUI_onDungeonSelect.Invoke(dungeonIndex);
    }
}
