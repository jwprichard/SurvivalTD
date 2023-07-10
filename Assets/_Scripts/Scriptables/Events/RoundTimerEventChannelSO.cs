using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Buildings/Round Timer Event Channel")]
public class RoundTimerEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised = delegate { };

    public void RaiseEvent()
    {
        OnEventRaised.Invoke();
    }
}
