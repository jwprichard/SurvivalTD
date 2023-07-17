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
            Timer.OnTimerElapsed += Attack;
        }

        public virtual void Update() 
        {
            ActionPerFrame();
        }

        //Called every frame by update
        public virtual void ActionPerFrame()
        {
            // If the unit's combat activity is disabled.
            if (!Unit.Active) return;
            CheckStats();
            FindTarget();
            CheckTargetInRange();
        }

        public void CheckStats()
        {
            if (Unit.Stats.Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public virtual void FindTarget()
        {
            if (Unit.Target != null) return;

            Transform target = GameObjectUtilities.FindTransformInDistance(transform, (int)Unit.Scriptable.targetUnit, 300);

            Unit.SetTarget(target);

        }

        private void CheckTargetInRange()
        {
            if (Unit.Target == null) return;

            if (!(Vector2.Distance(Unit.Target.position, transform.position) < Unit.Stats.Range)) return;

            HandleCombat();
        }

        private void HandleCombat()
        {
            if (Timer.isRunning) return;

            Timer.Init(Unit.Stats.AttackSpeed);
        }

        public virtual void TakeDamage(float damage)
        {
            UnitStats stats = Unit.Stats;
            stats.Health -= damage;
            Unit.SetStats(stats);
        }

        public virtual void Attack() { }
    }
}
