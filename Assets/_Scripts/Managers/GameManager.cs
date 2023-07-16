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
    [SerializeField] private BuildingEventChannelSO _onBuildingEvent = default;

    [Header("Listening to")]
    [SerializeField] private RoundTimerEventChannelSO _onRoundStart = default;
    [SerializeField] private BuildingEventChannelSO _onBuildingBuilt = default;
    //[SerializeField] private EnemyManagerStateECSO _onEnemyStateChange = default;

    public GameState State { get; private set; }

    private void OnEnable()
    {
        _onRoundStart.OnEventRaised += HandleRoundStart;
        _onBuildingBuilt.OnBuild += HandleBuildingBuilt;
        //_onEnemyStateChange.OnEventRaised += HandleEnemyStateChange;
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
            case GameState.Finish:
                Debug.Log("Wave...");
                HandleFinish();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), newState, null);
        }
    }

    // Event Handlers
    private void HandleBuildingBuilt(BuildingType buildingType)
    {
        if (!buildingType.Equals(BuildingType.Base)) return;
        ChangeState(GameState.Preparation);
    }

    //private void HandleEnemyStateChange(EnemyManagerState enemyState)
    //{
    //    if (!enemyState.Equals(EnemyManagerState.Finished)) return;
    //    ChangeState(GameState.Preparation);
    //}

    public void HandleRoundStart(GameState gameState) => ChangeState(gameState);

    // State Handlers
    private void HandleInitialise()
    {
        ChangeState(GameState.Starting);
    }

    private void HandleStarting()
    {
        _onBuildingEvent.RaiseBuildingChangeEvent(BuildingType.Base);
    }

    private void HandlePreparation()
    {
        // Pass
    }

    private void HandleWave()
    {
        // Pass
    }

    private void HandleFinish()
    {
        //Nothing
    }

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
