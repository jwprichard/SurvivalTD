using Assets.Units;
using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerMovementController MovementController => (PlayerMovementController) movementController;

}
