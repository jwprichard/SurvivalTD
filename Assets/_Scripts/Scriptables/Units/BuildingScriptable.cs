using System;
using UnityEngine;

namespace Assets.Scriptables.Units
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Building", menuName = "Building")]
    public class BuildingScriptable : UnitScriptable
    {
        [SerializeField] public ProjectileScriptable DefaultAmmo;
        public BuildingType buildingType;
    }

    public enum BuildingType
    {
        Base = 0,
        GunTurret = 1,
    }
}
