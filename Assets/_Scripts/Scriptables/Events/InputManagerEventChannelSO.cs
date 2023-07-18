using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Buildings/Select Event Channel")]
public class InputManagerEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised = delegate { };

    public UnityAction<InteractionType> OnInteractionTypeChangeRaised = delegate { };

    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }

    public void RaiseInteractionTypeChangeEvent(string interaction)
    {
        Enum.TryParse(interaction, out InteractionType interactionType);
        OnInteractionTypeChangeRaised.Invoke(interactionType);
    }

    public void RaiseInteractionTypeChangeEvent(InteractionType interactionType)
    {
        OnInteractionTypeChangeRaised.Invoke(interactionType);
    }
}
