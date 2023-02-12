using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Units
{
    public class PlayerMovementController : BaseMovementController
    {
        private float Horizontal = 0;
        private float Vertical = 0;

        public override void Move()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");

            transform.position += Unit.Stats.Speed * Time.deltaTime * new Vector3(Horizontal, Vertical, 0).normalized;
        }

        public override void Rotate()
        {

        }
    }
}
