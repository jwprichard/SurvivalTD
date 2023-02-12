using System;
using UnityEngine;

namespace Assets.Scriptables.Units
{
    [Serializable]
    public class UnitScriptableBase : ScriptableObject
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
        turret = 9,
        Enemy = 10,
        Ammo = 11,
    }
}
