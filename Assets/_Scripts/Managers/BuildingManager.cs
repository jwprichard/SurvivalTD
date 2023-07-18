using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;
using System.Threading.Tasks;

public class BuildingManager : Singleton<BuildingManager>
{
    [Header("Broadcasting On")]
    [SerializeField] private BuildingEventChannelSO _onBuild = default;
    [SerializeField] private BuildingEventChannelSO _onBuildingDestroyed = default;

    [Header("Listening to")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;
    [SerializeField] private BuildingEventChannelSO _onBuildingEvent = default;

    [SerializeField] private BuildingType Building;
    private GameObject BuildingGO;

    public BuildState State;

    private void OnEnable()
    {
        _onBuildingEvent.OnBuildInteract += HandleBuildInteractionEvent;
        _onBuildingEvent.OnBuildingChange += SetBuilding;
        _onGameStateChange.OnEventRaised += HandleGameStateChange;
    }

    public void ChangeState(BuildState newState) => State = newState;

    public void SetBuilding(BuildingType building) => Building = building;

    private void HandleBuildInteractionEvent()
    {
       BuildAction(Building);
    }

    private void HandleGameStateChange(GameState gameState)
    {
        if (!gameState.Equals(GameState.Preparation)) return;
        SetBuilding(BuildingType.GunTurret);
    }

    // Builds a specific building by passing in a BuildingType building
    public void BuildAction(BuildingType building)
    {
        BuildingGO = Instantiate(ResourceSystem.Instance.GetBuilding(building).prefab);

        MapTile tile = MapManager.Instance.MapGrid.GetGridObject(InputUtilities.GetMouseWorldPosition());

        if (tile == null) return;


        BuildingGO.transform.position = tile.GetCenterPosition();

        if (tile.GetUnit() != null)
        {
            Debug.Log("Building already exists!");
            return;
        }
        //BuildingGO.GetComponent<Unit>().Active = true;
        tile.SetUnit(BuildingGO);
        _onBuild.OnBuild(building);
    }

}

public enum BuildState
{
    GameStart,
    Building,
    Idle,
}
