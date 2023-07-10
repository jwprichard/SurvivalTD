using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Buildings/Game State Event Channel")]
public class GameStateEventChannelSO : ScriptableObject
{
    public UnityAction<GameState> OnEventRaised = delegate { };

    public void RaiseEvent(GameState state)
    {
        OnEventRaised.Invoke(state);
    }
}

