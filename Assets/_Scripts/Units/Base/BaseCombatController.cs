using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;

namespace Assets.Units
{
    public class BaseCombatController : MonoBehaviour
    {
        [HideInInspector] public UnitBase Unit { get; private set; }
        [HideInInspector] public AttackTimer Timer;

        //public WeaponScriptable Weapon;
        public Transform AttackPoint;

        public virtual void Awake()
        {
            Unit = GetComponent<UnitBase>();
            Timer = gameObject.AddComponent<AttackTimer>();
        }

        public virtual void Update()
        {
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
            float minDistance = int.MaxValue;
            Vector2 position = gameObject.transform.position;
            Transform target = null;

            Collider2D[] collisions = Physics2D.OverlapCircleAll(position, Unit.Stats.Range, 1 << (int) Unit.Scriptable.targetUnit);
            if (collisions.Length > 0)
            {
                foreach (Collider2D collision in collisions)
                {
                    float newDistance = Vector2.Distance(position, collision.transform.position);
                    if (newDistance < minDistance)
                    {
                        minDistance = newDistance;
                        target = collision.transform;
                    }
                }
            }

            if (target != null)
            {
                Unit.SetTarget(target);
            }
        }
    }
}
