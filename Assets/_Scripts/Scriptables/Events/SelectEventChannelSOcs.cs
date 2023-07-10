using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Buildings/Select Event Channel")]
public class SelectEventChannelSOcs : ScriptableObject
{
    public UnityAction OnEventRaised = delegate { };

    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }
}
