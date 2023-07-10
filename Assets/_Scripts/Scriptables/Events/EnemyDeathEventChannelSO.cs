using UnityEngine;
using UnityEngine.Events;
using Assets.Units;

[CreateAssetMenu(menuName = "Events/EventChannels/Enemy Death Event Channel")]
public class EnemyDeathEventChannelSO : ScriptableObject
{
    public UnityAction<Enemy> OnEventRaised = delegate { };

    public void RaiseEvent(Enemy enemy)
    {
        OnEventRaised.Invoke(enemy);
    }
}

