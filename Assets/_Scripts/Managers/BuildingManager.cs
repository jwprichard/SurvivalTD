using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;

public class BuildingManager : Singleton<BuildingManager>
{
    [Header("Broadcasting On")]
    [SerializeField] private BuildingEventChannelSO _onBuild = default;

    [Header("Listening to")]
    [SerializeField] private BuildingEventChannelSO _onBuildInteract = default;

    [SerializeField] private BuildingType Building;
    private GameObject BuildingGO;

    public BuildState State;

    private void OnEnable()
    {
        _onBuildInteract.OnBuildInteract += Build;
    }

    public void Update()
    { 
        CheckState(); 
    }

    public void ChangeState(BuildState newState) => State = newState;

    public void SetBuilding(BuildingType building) => Building = building;

    private void CheckState()
    {
        switch (State)
        {
            case BuildState.Building:
                break;
            case BuildState.Idle:
                break;
            default:
                throw new ArgumentOutOfRangeException("State is out of range.");
        }
    }

    public void Build()
    {
        BuildingGO = Instantiate(ResourceSystem.Instance.GetBuilding(Building).prefab);

        MapTile tile = MapManager.Instance.MapGrid.GetGridObject(InputUtilities.GetMouseWorldPosition());

        if (tile == null) return;


        BuildingGO.transform.position = tile.GetCenterPosition();

        if (tile.GetUnit() != null)
        {
            Debug.Log("Building already exists!");
            return;
        }
        BuildingGO.GetComponent<Unit>().Active = true;
        tile.SetUnit(BuildingGO);
        _onBuild.OnBuild(Building);
    }

}

public enum BuildState
{
    Building,
    Idle,
}
