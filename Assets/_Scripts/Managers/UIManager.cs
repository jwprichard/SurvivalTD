using UnityEngine;
using TMPro;
using Assets.Scripts.Utilities;
using System.Runtime.CompilerServices;

public class UIManager : MonoBehaviour
{
    [Header("Broadcasting On")]
    [SerializeField] private RoundTimerEventChannelSO _onRoundTimerElapsed = default;

    [Header("Listening On")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;

    [Space]
    [SerializeField] private TMP_Text TextTimer;

    private TimerState CurrentState;

    private SimpleTimer PrepTimer;
    private SimpleTimer WaveTimer;

    private void OnEnable()
    {
        _onGameStateChange.OnEventRaised += StartTimer;
        SetupTimers();
    }

    public void Update()
    {
        if (TextTimer.enabled)
        {
            if (CurrentState.Equals(TimerState.Prep)) TextTimer.text = string.Format("{0:00}", PrepTimer.Time);
            if (CurrentState.Equals(TimerState.Wave)) TextTimer.text = string.Format("{0:00}", WaveTimer.Time);
        }
    }

    private void ChangeState(TimerState state) => CurrentState = state;

    private void SetupTimers()
    {
        PrepTimer = gameObject.AddComponent<SimpleTimer>();
        PrepTimer.OnTimerElapsed += RoundStart;
        WaveTimer = gameObject.AddComponent<SimpleTimer>();
        WaveTimer.OnTimerElapsed += RoundFinished;
    }

    private void StartTimer(GameState state)
    {
        if (state.Equals(GameState.Preparation))
        {
            TextTimer.enabled = true;
            TextTimer.color = Color.green;
            PrepTimer.Init(10f);
        } 

        if (state.Equals(GameState.Wave))
        {
            TextTimer.enabled = true;
            TextTimer.color = Color.red;
            WaveTimer.Init(10f);
        }
    }

    private void RoundStart()
    {
        _onRoundTimerElapsed.RaiseEvent(GameState.Wave);
        ChangeState(TimerState.Wave);
    }

    private void RoundFinished()
    {
        _onRoundTimerElapsed.RaiseEvent(GameState.Preparation);
        ChangeState(TimerState.Prep);
    }
}

public enum TimerState
{
    Prep = 0,
    Wave = 1,
}