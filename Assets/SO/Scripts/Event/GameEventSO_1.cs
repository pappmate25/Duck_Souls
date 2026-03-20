using System;
using UnityEngine;

public abstract class GameEventSO<T> : ScriptableObject
{
    private Action<T> onEvent;

    public void Invoke(T value)
    {
        onEvent?.Invoke(value);
    }

    public void Subscribe(Action<T> listener) => onEvent += listener;
    public void UnSubscribe(Action<T> listener) => onEvent -= listener;
}
