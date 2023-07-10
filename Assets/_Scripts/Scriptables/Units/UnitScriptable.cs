using System;
using UnityEngine;

namespace Assets.Scriptables.Units
{
    [Serializable]
    public class UnitScriptable : ScriptableObject
    {
        [SerializeField]
        new string name;
        public UnitType unitType;
        public UnitType targetUnit;

        [SerializeField] private UnitStats _stats;
        public UnitStats BaseStats => _stats;

        public GameObject prefab;
    }

    [Serializable]
    public struct UnitStats
    {
        public float Health;
        public float Speed;
        public float Range;
        public float Damage;
    }
    [Serializable]
    public enum UnitType
    {
        Player = 6,
        Building = 10,
        Enemy = 11,
        Projectile = 15,
    }
}
