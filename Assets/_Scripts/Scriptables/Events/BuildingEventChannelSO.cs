using UnityEngine;
using UnityEngine.Events;
using Assets.Scriptables.Units;
using System;

[CreateAssetMenu(menuName = "Events/Buildings/Building Event Channel")]
public class BuildingEventChannelSO : ScriptableObject
{
    public UnityAction OnBuildInteract = delegate { };

    public UnityAction<BuildingType> OnBuild = delegate { };

    public UnityAction<BuildingType> OnBuildingChange = delegate { };

    public UnityAction<BuildingType> OnBuildingDestroyed = delegate { };

    public void RaiseBuildInteractEvent()
    {
        OnBuildInteract.Invoke();
    }

    public void RaiseBuildEvent(BuildingType building)
    {
        OnBuild.Invoke(building);
    }

    public void RaiseBuildingChangeEvent(string building)
    {
        Enum.TryParse(building, out BuildingType buildingType);
        OnBuildingChange.Invoke(buildingType);
    }

    public void RaiseBuildingChangeEvent(BuildingType building)
    {
        OnBuildingChange.Invoke(building);
    }

    public void RaiseOnBuildingDestroyed(BuildingType building)
    {
        OnBuildingDestroyed.Invoke(building);
    }
}

