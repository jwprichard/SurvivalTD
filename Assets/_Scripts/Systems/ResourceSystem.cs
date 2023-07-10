using UnityEngine;
using Assets.Scriptables.Units;
using System.Collections.Generic;
using System.Linq;

public class ResourceSystem : Singleton<ResourceSystem>
{
    //Buildings
    public List<BuildingScriptable> Buildings { get; private set; }
    private Dictionary<BuildingType, BuildingScriptable> _buildings;

    //Enemies
    public List<EnemyScriptable> Enemies { get; private set; }
    private Dictionary<EnemyType, EnemyScriptable> _enemies;

    //Projectiles
    public List<ProjectileScriptable> Projectiles { get; private set; }
    private Dictionary<ProjectileType, ProjectileScriptable> _projectiles;

    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        Buildings = Resources.LoadAll<BuildingScriptable>("ScriptableObjects/Buildings").ToList();
        _buildings = Buildings.ToDictionary(r => r.buildingType, r => r);

        Enemies = Resources.LoadAll<EnemyScriptable>("ScriptableObjects/Enemies").ToList();
        _enemies = Enemies.ToDictionary(r => r.enemyType, r => r);

        Projectiles = Resources.LoadAll<ProjectileScriptable>("ScriptableObjects/Projectiles").ToList();
        _projectiles = Projectiles.ToDictionary(r => r.projectileType, r => r);
    }

    public BuildingScriptable GetBuilding(BuildingType t) => _buildings[t];
    public EnemyScriptable GetEnemy(EnemyType t) => _enemies[t];
    public ProjectileScriptable GetProjectile(ProjectileType t) => _projectiles[t];
}
