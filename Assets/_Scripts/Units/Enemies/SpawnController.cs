using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private SpawnState CurrentState;

    private SimpleTimer spawnTimer;

    private List<Enemy> Enemies;

    private int SpawnFrequency;
    private int MaxSpawnPoints;
    private int CurrentSpawnPoints;

    public void Init(int maxSpawnPoints, int spawnFrequency)
    {
        MaxSpawnPoints = maxSpawnPoints;
        SpawnFrequency = spawnFrequency;
        Enemies = new();
        SetupTimer();
        ChangeState(SpawnState.Idle);
    }

    public void SetupTimer()
    {
        spawnTimer = gameObject.AddComponent<SimpleTimer>();
        spawnTimer.OnTimerElapsed += HandleSpawning;
    }

    public void ChangeState(SpawnState newState)
    {

        // Event channel for spawn here if needed

        CurrentState = newState;
        switch (CurrentState)
        {
            case SpawnState.Spawning:
                CurrentSpawnPoints = MaxSpawnPoints;
                spawnTimer.Init(SpawnFrequency);
                break;
            case SpawnState.Idle: break;
            default:
                throw new ArgumentOutOfRangeException("State is out of range.");
        }

    }

    private void HandleSpawning()
    {
        Spawn();
        if (CurrentSpawnPoints <= 0)
        {
            ChangeState(SpawnState.Idle);
            return;
        }
        spawnTimer.Init(SpawnFrequency);
    }

    private void Spawn()
    {

        Vector2 randomPos =
            new(UnityEngine.Random.Range(-VariableManager.Instance.Width, VariableManager.Instance.Width),
                UnityEngine.Random.Range(-VariableManager.Instance.Height, VariableManager.Instance.Height));
        GameObject enemy = Instantiate(ResourceSystem.Instance.GetEnemy(EnemyType.eye).prefab);
        Enemies.Add(enemy.GetComponent<Enemy>());
        CurrentSpawnPoints -= enemy.GetComponent<Enemy>().Scriptable.SpawnCost;
        enemy.transform.position = randomPos;
    }

}

public enum SpawnState
{
    Spawning = 0,
    Idle = 1,
}

