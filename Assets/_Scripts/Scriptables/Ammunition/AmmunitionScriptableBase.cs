using System;
using UnityEngine;

namespace Assets.Scriptables.Units
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Ammo", menuName = "Ammunition")]
    public class AmmunitionScriptableBase : UnitScriptableBase
    {
        public AmmoType ammoType;
    }

    public enum AmmoType
    {
        Basic
    }
}
