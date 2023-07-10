using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class Projectile : Unit
    {

        private Vector2 PreviousPosition;

        public override void SetupTimer()
        {
            base.SetupTimer();
            SimpleTimer.Init(0.25f, true);
            SimpleTimer.OnTimerElapsed += VerifyMovement;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CombatController bcc))
            {
                if (bcc.Unit.gameObject.layer == Target.gameObject.layer)
                {
                    bcc.TakeDamage(Stats.Damage);
                    Destroy(gameObject);
                }
            }
        }

        // Verify the the game object is moving, if the gameobject is not moving then it has reached its target
        // and should be destroyed.
        private void VerifyMovement()
        {
            Vector2 currentPosition = gameObject.transform.position;

            if (currentPosition != PreviousPosition) PreviousPosition = currentPosition;

            Destroy(gameObject);
        }
    }
}
