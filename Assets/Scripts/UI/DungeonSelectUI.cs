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

    //events
    [SerializeField] private GameEventIntSO onDungeonSelect;
    [SerializeField] private GameEventSO onInteraction;

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
        onInteraction.Subscribe(ActivateDungeonUI);

        dungeonButton1.clicked += Dungeon1;
        dungeonButton2.clicked += Dungeon2;
        dungeonButton3.clicked += Dungeon3;

    }

    private void OnDisable()
    {
        onInteraction.UnSubscribe(ActivateDungeonUI);

        dungeonButton1.clicked -= Dungeon1;
        dungeonButton2.clicked -= Dungeon2;
        dungeonButton3.clicked -= Dungeon3;
    }

    private void ActivateDungeonUI()
    {
        uiManager.OpenUI(root, false);
    }

    private void Dungeon1() => DungeonSelect(1);
    private void Dungeon2() => DungeonSelect(2);
    private void Dungeon3() => DungeonSelect(3);
    private void DungeonSelect(int dungeonIndex)
    {
        onDungeonSelect.Invoke(dungeonIndex);
    }
}
