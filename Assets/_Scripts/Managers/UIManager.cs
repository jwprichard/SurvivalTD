using UnityEngine;
using TMPro;
using Assets.Scripts.Utilities;

public class UIManager : MonoBehaviour
{
    [Header("Broadcasting On")]
    [SerializeField] private RoundTimerEventChannelSO _onRoundTimerElapsed = default;

    [Header("Listening On")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;

    [Space]
    [SerializeField] private TMP_Text TextTimer;

    private SimpleTimer Timer;

    private void OnEnable()
    {
        _onGameStateChange.OnEventRaised += StartTimer;
        Timer = gameObject.AddComponent<SimpleTimer>();
        Timer.OnTimerElapsed += RoundStart;
    }

    public void Update()
    {
        if (TextTimer.enabled) TextTimer.text = Timer.time.ToString();
    }

    private void StartTimer(GameState state)
    {
        if (state != GameState.Preparation) return;
        TextTimer.enabled = true;
        Timer.Init(2f);
    }
    
    private void RoundStart()
    {
        _onRoundTimerElapsed.RaiseEvent();
    }
}