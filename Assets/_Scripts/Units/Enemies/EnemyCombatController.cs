using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;

namespace Assets.Units
{
    public class EnemyCombatController : CombatController
    {
        public override void Attack()
        {
            Unit.Target.GetComponent<CombatController>().TakeDamage(Unit.Stats.Damage);
        }
    }
}
