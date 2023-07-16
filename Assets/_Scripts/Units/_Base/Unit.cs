using UnityEngine;
using Assets.Scriptables.Units;
using Assets.Scripts.Utilities;

namespace Assets.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] public virtual UnitStats Stats { get; private set; }
        [SerializeField] public Transform Target { get; private set; }
        [SerializeField] public UnitScriptable Scriptable;
        [HideInInspector] public MovementController movementController;
        [HideInInspector] public CombatController combatController;
        [HideInInspector] public SimpleTimer SimpleTimer;
        [SerializeField] private bool _active = true;
        public bool Active { get => _active; set => _active = value; }

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

        public virtual void SetupTimer()
        {
            SimpleTimer = gameObject.AddComponent<SimpleTimer>();
        }

        public virtual void SetStats(UnitStats stats) => Stats = stats;

        public virtual void SetTarget(Transform target) => Target = target;

        public virtual void OnDestroy()
        {
            transform.DestroyAllChildren();
        }
    }
}
