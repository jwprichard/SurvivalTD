using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;

namespace Assets.Units
{
    public class CombatController : MonoBehaviour
    {
        [HideInInspector] public Unit Unit { get; private set; }
        [HideInInspector] public AttackTimer Timer;

        //public WeaponScriptable Weapon;
        public Transform AttackPoint;

        public virtual void Awake()
        {
            Unit = GetComponent<Unit>();
            Timer = gameObject.AddComponent<AttackTimer>();
        }

        public virtual void Update()
        {
            // If the unit's combat activity is disabled.
            if (!Unit.Active) return;
            FindTarget();
            CheckStats();
        }

        public void CheckStats()
        {
            if (Unit.Stats.Health  <= 0)
            {
                Destroy(gameObject);
            }
        }

        public virtual void TakeDamage(float damage)
        {
            UnitStats stats = Unit.Stats;
            stats.Health -= damage;
            Unit.SetStats(stats);
        }

        public virtual void Attack() { }

        public virtual void FindTarget()
        {
            Transform target = GameObjectUtilities.FindTransformInDistance(transform, (int)Unit.Scriptable.targetUnit, Unit.Stats.Range);

            if (target != null)
            {
                Unit.SetTarget(target);
            }
        }
    }
}
