using System;
using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;
using Assets.Units;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
    [Header("Listening To")]
    [SerializeField] private EnemyDeathEventChannelSO _onEnemyDeath = default;
    [SerializeField] private GameStateEventChannelSO _onWaveStart = default;

    public SpawnState State;
    private SimpleTimer Timer;
    private List<Enemy> Enemies;

    private float SpawnTime = 2f;

    private int width = 20;
    private int height = 20;

    private int SpawnPoints = 10;

    private void OnEnable()
    {
        _onEnemyDeath.OnEventRaised += OnEnemyDeath;
        _onWaveStart.OnEventRaised += HandleSpawning;
    }

    public void Start()
    {
        SetupTimer();
        ChangeState(SpawnState.Idle);
        Enemies = new List<Enemy>();
    }

    private void SetupTimer()
    {
        Timer = gameObject.AddComponent<SimpleTimer>();
        Timer.OnTimerElapsed += SpawnEnemy;
    }

    public void ChangeState(SpawnState newState)
    {
        State = newState;
        //switch (newState)
        //{
        //    case SpawnState.Spawning:
        //        Timer.Init(SpawnTime, true);
        //        StartCoroutine(HandleSpawning());
        //        GameManager.Instance.ChangeState(GameState.Upgrade);
        //        break;
        //    case SpawnState.Idle:
        //        break;
        //    default:
        //        throw new ArgumentOutOfRangeException("State is out of range.");
        //}
    }

    private void HandleSpawning(GameState state)
    {
        if (state != GameState.Wave) return;

        StartCoroutine(SpawnEnemies());

        if (SpawnPoints <= 0)
        {
            ChangeState(SpawnState.Idle);
            while (Enemies.Count > 0) { }
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (SpawnPoints > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(SpawnTime);
        }
        yield return null;
    }

    private void SpawnEnemy()
    {
        Vector2 randomPos = new(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height));
        GameObject enemy = Instantiate(ResourceSystem.Instance.GetEnemy(EnemyType.eye).prefab);
        Enemies.Add(enemy.GetComponent<Enemy>());
        SpawnPoints -= enemy.GetComponent<Enemy>().Scriptable.SpawnCost;
        enemy.transform.position = randomPos;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }
}

public enum SpawnState
{
    Spawning,
    Idle,
}
