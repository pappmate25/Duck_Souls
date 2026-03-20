using System;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName ="SO/Events/GameEvent")]
public class GameEventSO : ScriptableObject
{
    private Action onEvent;
    
    public void Invoke()
    {
        onEvent?.Invoke();
    }

    public void Subscribe(Action listener) => onEvent += listener;
    public void UnSubscribe(Action listener) => onEvent -= listener;
}
