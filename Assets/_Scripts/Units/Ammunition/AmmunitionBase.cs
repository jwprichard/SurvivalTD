using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class AmmunitionBase : UnitBase
    {
        public Vector2 TargetLocation;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.TryGetComponent(out BaseCombatController bcc))
            {
                if (bcc.Unit.gameObject.layer == Target.gameObject.layer)
                {
                    bcc.TakeDamage(Stats.Damage);
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseCombatController bcc))
            {
                if (bcc.Unit.gameObject.layer == Target.gameObject.layer)
                {
                    bcc.TakeDamage(Stats.Damage);
                    Destroy(gameObject);
                }
            }
        }

        public override void SetTarget(Transform target)
        {
            base.SetTarget(target);
            TargetLocation = target.position;
        }

        public override Vector2 GetTargetLocation() => TargetLocation;
    }
}
