using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Units
{
    public class BuildingMovementController : MovementController
    {
        public override void Update()
        {
            base.Rotate();
        }
    }
}
