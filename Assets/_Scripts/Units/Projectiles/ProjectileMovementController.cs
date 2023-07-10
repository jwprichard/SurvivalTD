using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Units;

public class ProjectileMovementController : MovementController
{
    private Vector2 Target;

    public void Start()
    {
        Target = Unit.Target.position;    
    }

    public override void Move()
    {
        float step = Unit.Stats.Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Target, step);
    }
}
