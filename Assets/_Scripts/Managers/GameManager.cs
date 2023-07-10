using System;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.Utilities;
using Assets.Scriptables.Units;

public class GameManager : Singleton<GameManager>
{
    [Header("Broadcasting on")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;

    [Header("Listening to")]
    [SerializeField] private RoundTimerEventChannelSO _onRoundStart = default;
    [SerializeField] private BuildingEventChannelSO _onBuild = default;

    public GameState State { get; private set; }

    private void OnEnable()
    {
        _onRoundStart.OnEventRaised += StartRound;
        //_onBuild.OnBuild += { }
    }

    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        _onGameStateChange.RaiseEvent(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                Debug.Log("Starting...");
                HandleStarting();
                break;
            case GameState.BeforeWave:
                Debug.Log("BeforeWave...");
                HandleBeforeWave();
                break;
            case GameState.Wave:
                Debug.Log("Wave...");
                break;
            case GameState.Upgrade:
                break;
            case GameState.Finish:
                HandleFinish();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), newState, null);
        }
    }

    private void HandleStarting()
    {
        BuildingManager.Instance.ChangeState(BuildState.Building);
        BuildingManager.Instance.SetBuilding(BuildingType.Base);
    }

    private void HandleBeforeWave()
    {
        BuildingManager.Instance.ChangeState(BuildState.Building);
        BuildingManager.Instance.SetBuilding(BuildingType.GunTurret);
    }

    private void HandleWave()
    {
        EnemyManager.Instance.ChangeState(SpawnState.Spawning);
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
    Starting,
    BeforeWave,
    Wave,
    Upgrade,
    Finish,
}
