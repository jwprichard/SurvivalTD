using System;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Utilities;
using Assets.Scriptables.Units;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    [Header("Broadcasting on")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;

    [Header("Listening to")]
    [SerializeField] private RoundTimerEventChannelSO _onRoundStart = default;
    [SerializeField] private BuildingEventChannelSO _onBuildingBuilt = default;

    public GameState State { get; private set; }

    private void OnEnable()
    {
        _onRoundStart.OnEventRaised += StartRound;
        _onBuildingBuilt.OnBuild += HandleBuildingBuilt;
    }

    private void Start() => ChangeState(GameState.Initialise);

    public void ChangeState(GameState newState)
    {
        _onGameStateChange.RaiseEvent(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Initialise:
                Debug.Log("Initialising...");
                HandleInitialise();
                break;
            case GameState.Starting:
                Debug.Log("Starting...");
                HandleStarting();
                break;
            case GameState.Preparation:
                Debug.Log("Preparation...");
                HandlePreparation();
                break;
            case GameState.Wave:
                Debug.Log("Wave...");
                break;
            //case GameState.Upgrade:
            //    break;
            //case GameState.Finish:
            //    HandleFinish();
            //    break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), newState, null);
        }
    }

    private void HandleBuildingBuilt(BuildingType buildingType)
    {
        if (!buildingType.Equals(BuildingType.Base)) return;
        ChangeState(GameState.Preparation);
    }

    private void HandleInitialise()
    {
        ChangeState(GameState.Starting);
    }

    private void HandleStarting()
    {
        // Pass
    }

    private void HandlePreparation()
    {
        //BuildingManager.Instance.ChangeState(BuildState.Building);
        //BuildingManager.Instance.SetBuilding(BuildingType.GunTurret);
    }

    private void HandleWave()
    {
        //EnemyManager.Instance.ChangeState(SpawnState.Spawning);
    }

    private void HandleUpgrade()
    {
        //Nothing
    }

    private void HandleFinish()
    {
        //Nothing
    }

    public void StartRound() => ChangeState(GameState.Wave);

}

[Serializable]
public enum GameState
{
    Initialise,
    Starting,
    Preparation,
    Wave,
    Upgrade,
    Finish,
}
