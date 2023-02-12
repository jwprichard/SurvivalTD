using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class UnitBase : MonoBehaviour
    {
        [SerializeField] public virtual UnitStats Stats { get; private set; }
        [HideInInspector] public Transform Target { get; private set; }
        [SerializeField] public UnitScriptableBase Scriptable;
        [HideInInspector] public BaseMovementController movementController;
        [HideInInspector] public BaseCombatController combatController;

        // If the scriptable base != null then assign the stats of the scriptable base the the GO
        public virtual void Awake()
        {
            if (Scriptable != null)
            {
                SetStats(Scriptable.BaseStats);
            }
            TryGetComponent(out movementController);
            TryGetComponent(out combatController);
        }

        public virtual void SetStats(UnitStats stats) => Stats = stats;

        public virtual void SetTarget(Transform target) => Target = target;

        public virtual Vector2 GetTargetLocation() => Target.position;
    }
}
