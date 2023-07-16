using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class Building : Unit
    {
        public BuildingCombatController BuildingCombatController => (BuildingCombatController) combatController;
        public new BuildingScriptable Scriptable => (BuildingScriptable) base.Scriptable;
        
        public void Start()
        {
            //Active = false;
            // Set the ammo for the combat controller
            if (Scriptable != null) BuildingCombatController.SetAmmo(Scriptable.DefaultAmmo);
        }
    }
}
