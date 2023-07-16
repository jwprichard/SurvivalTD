using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;
using System;
using System.Collections;
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
        StartCoroutine(Spawn());
        if (CurrentSpawnPoints <= 0)
        {
            ChangeState(SpawnState.Idle);
            return;
        }
        spawnTimer.Init(SpawnFrequency);
    }

    private IEnumerator Spawn()
    {
        Vector2 randomPos = new();
        for (int i = 0; i < 100; i++)
        {
            randomPos =
            new(UnityEngine.Random.Range(-VariableManager.Instance.Width * 3, VariableManager.Instance.Width * 3),
                UnityEngine.Random.Range(-VariableManager.Instance.Height * 3, VariableManager.Instance.Height * 3));

            if (CheckBuildingDistance(randomPos))
            {
                Debug.Log("Spawn calculated in " + i + " tries.");
                break; 
            }
            if (i == 99)
            {
                Debug.Log("Hit maximum retry, not spawning enemy!");
                yield return null;
            }
        }

        GameObject enemy = Instantiate(ResourceSystem.Instance.GetEnemy(EnemyType.eye).prefab);
        Enemies.Add(enemy.GetComponent<Enemy>());
        CurrentSpawnPoints -= enemy.GetComponent<Enemy>().Scriptable.SpawnCost;
        enemy.transform.position = randomPos;
        yield return null;
    }

    private bool CheckBuildingDistance(Vector2 position)
    {
        Transform building = GameObjectUtilities.FindTransformInDistance(position, (int)UnitType.Building, 20);

        if (building != null)  return false; 
        return true;
    }

}

public enum SpawnState
{
    Spawning = 0,
    Idle = 1,
}

