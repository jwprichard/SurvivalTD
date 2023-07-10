using UnityEngine;
using UnityEngine.Events;
using Assets.Scriptables.Units;

[CreateAssetMenu(menuName = "Events/Buildings/Building Event Channel")]
public class BuildingEventChannelSO : ScriptableObject
{
    public UnityAction OnBuildInteract = delegate { };

    public UnityAction<BuildingType> OnBuild = delegate { };

    public void RaiseBuildInteractEvent()
    {
        OnBuildInteract.Invoke();
    }

    public void RaiseBuildEvent(BuildingType building)
    {
        OnBuild.Invoke(building);
    }
}

