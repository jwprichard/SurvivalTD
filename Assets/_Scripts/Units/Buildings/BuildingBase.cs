using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class BuildingBase : UnitBase
    {
        public AmmunitionScriptableBase Ammo;
        public BuildingCombatController BuildingCombatController => (BuildingCombatController) combatController;
        

        public void Start()
        {
            BuildingCombatController.SetAmmo(Ammo);
        }
    }
}
