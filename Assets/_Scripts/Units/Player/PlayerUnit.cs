using Assets.Units;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    public PlayerMovementController MovementController => (PlayerMovementController) movementController;

}
