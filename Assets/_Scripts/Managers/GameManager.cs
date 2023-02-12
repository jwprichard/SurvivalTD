using System;
using UnityEngine;
using Assets.Scripts.Utilities;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case GameState.Starting:
                HandleStarting();
                break;
        }
    }

    private void HandleStarting()
    {
        //Eventually call ChangeState for the next state
        MapManager.Instance.Initialize();
    }

}

[Serializable]
public enum GameState
{
    Starting,
}
