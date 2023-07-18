using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private SpawnState CurrentState;

    private SimpleTimer spawnTimer;
    private SimpleTimer SpawnPointIncrementor;

    private List<Enemy> Enemies;

    private float SpawnInterval;
    private float MaxSpawnInterval;

    [SerializeField] private float CurrentSpawnPoints;
    private float TimeSinceLastSpawn;

    public void Init(float spawnInterval, float maxSpawnInterval)
    {
        SpawnInterval = spawnInterval;
        MaxSpawnInterval = maxSpawnInterval;
        TimeSinceLastSpawn = 0;
        Enemies = new();
        //InitTimers();
    }

    public void InitTimers()
    {
        spawnTimer = gameObject.AddComponent<SimpleTimer>();
        spawnTimer.OnTimerElapsed += HandleSpawning;
        spawnTimer.Init(SpawnInterval, true);

        SpawnPointIncrementor = gameObject.AddComponent<SimpleTimer>();
        SpawnPointIncrementor.OnTimerElapsed += AddSpawnPoints;
        SpawnPointIncrementor.Init(1, true);
    }

    private void HandleSpawning()
    {
        if (!DoSpawn()) return;

        StartCoroutine(Spawn());
    }

    // Perform a check to see if the spawner should spawn enemies or save points
    // for a bigger spawn.
    private bool DoSpawn()
    {
        TimeSinceLastSpawn += SpawnInterval;
        float spawnChance = TimeSinceLastSpawn / MaxSpawnInterval * 100;

        int num = UnityEngine.Random.Range(0, 101);
        if (num > spawnChance) return false;

        return true;
    }

    private void AddSpawnPoints()
    {
        CurrentSpawnPoints += VariableManager.SpawnPointIncrementConstant;
    }

    private IEnumerator Spawn()
    {
        if (!CalculateSpawnLocation(out Vector2 randomPos)) yield return null;



        GameObject[] enemies = EnemiesToSpawn();
        foreach (GameObject enemy in enemies)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            Enemies.Add(e);
            CurrentSpawnPoints -= e.Scriptable.SpawnCost;
            enemy.transform.position = new(randomPos.x * UnityEngine.Random.Range(1, 1.1f), randomPos.y * UnityEngine.Random.Range(1, 1.1f));
        }
        //enemy.transform.position = randomPos;
        yield return null;
    }

    // Using the available spawn points choose which enemies to spawn
    private GameObject[] EnemiesToSpawn()
    {
        List<EnemyType> enemies = new();
        //Loop through all enemy types and only add ones we can afford
        foreach (EnemyType et in Enum.GetValues(typeof(EnemyType)))
        {
            if ((int)et < CurrentSpawnPoints)
            {
                enemies.Add(et);
            }
        }

        int num = UnityEngine.Random.Range(0, enemies.Count);
        EnemyType chosenEnemy = enemies[num];
        int numToSpawn = ((int)CurrentSpawnPoints / (int)enemies[num]);

        GameObject[] enemyGameObjects = new GameObject[numToSpawn];

        for (int i = 0; i < numToSpawn; i++)
        {
            enemyGameObjects[i] = Instantiate(ResourceSystem.Instance.GetEnemy(chosenEnemy).prefab);
        }
        
        return enemyGameObjects;
    }

    private bool CalculateSpawnLocation(out Vector2 randomPos)
    {
        for (int i = 0; i < 100; i++)
        {
            randomPos =
            new(UnityEngine.Random.Range(-VariableManager.Width * 3, VariableManager.Width * 3),
                UnityEngine.Random.Range(-VariableManager.Height * 3, VariableManager.Height * 3));

            if (CheckBuildingDistance(randomPos))
            {
                Debug.Log("Spawn calculated in " + i + " tries.");
                return true;
            }
        }

        Debug.Log("Hit maximum retry, not spawning enemy!");
        randomPos = new(0, 0);
        return false;
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
    ReConfig = 1,
    Idle = 2,
}

