using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Broadcasting On")]
    [SerializeField] private EnemyManagerStateECSO _onEnemyManagerStateChange = default;

    [Header("Listening To")]
    [SerializeField] private EnemyDeathEventChannelSO _onEnemyDeath = default;
    [SerializeField] private GameStateEventChannelSO _onGameStateChange = default;

    private SpawnController[] spawnControllers = new SpawnController[1];


    private void OnEnable()
    {
        _onGameStateChange.OnEventRaised += HandleGameStateChange;
    }

    public void Start()
    {
        SetupSpawnControllers();
    }

    private void HandleGameStateChange(GameState gameState)
    {
        if (!gameState.Equals(GameState.Wave)) return;
        StartSpawners();
    }

    private void SetupSpawnControllers()
    {
        GameObject spawnController = new();
        spawnController.name = "SpawnController";
        spawnController.transform.parent = transform;
        spawnControllers[0] = spawnController.AddComponent<SpawnController>();
        spawnControllers[0].Init(10, 2);
    }

    private void StartSpawners()
    {
        foreach (SpawnController spawner in  spawnControllers)  spawner.ChangeState(SpawnState.Spawning);
    }
}