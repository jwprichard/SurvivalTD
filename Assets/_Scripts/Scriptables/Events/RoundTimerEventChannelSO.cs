using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Buildings/Round Timer Event Channel")]
public class RoundTimerEventChannelSO : ScriptableObject
{
    public UnityAction<GameState> OnEventRaised = delegate { };

    public void RaiseEvent(GameState gameState)
    {
        OnEventRaised.Invoke(gameState);
    }
}
