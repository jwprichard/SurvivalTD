using Assets.Scriptables.Units;
using UnityEngine;

namespace Assets.Units
{
    public class Building : Unit
    {
        [Header("Broadcasting To")]
        [SerializeField] private BuildingEventChannelSO _onBuildingDestroyed = default;

        public BuildingCombatController BuildingCombatController => (BuildingCombatController) combatController;
        public new BuildingScriptable Scriptable => (BuildingScriptable) base.Scriptable;
        
        public void Start()
        {
            //Active = false;
            // Set the ammo for the combat controller
            if (Scriptable != null) BuildingCombatController.SetAmmo(Scriptable.DefaultAmmo);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _onBuildingDestroyed.RaiseOnBuildingDestroyed(Scriptable.buildingType);
        }
    }
}
