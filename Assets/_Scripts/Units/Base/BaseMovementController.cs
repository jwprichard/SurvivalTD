using UnityEngine;
using Assets.Scripts.Utilities;

namespace Assets.Units
{
    public class BaseMovementController : MonoBehaviour
    {
        [HideInInspector] public UnitBase Unit;
        public GameObject rotationObject;

        public virtual void Awake()
        {
            Unit = GetComponent<UnitBase>();
        }

        public virtual void Update()
        {
            Move();
            Rotate();
        }

        public virtual void Move()
        {
            float step = Unit.Stats.Speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Unit.GetTargetLocation(), step);
        }

        public virtual void Rotate()
        {
            if (rotationObject != null)
            {
                rotationObject.transform.rotation = UtilsClass.LookAt(gameObject.transform.position, Unit.Target.position);
            } else
            {
                gameObject.transform.rotation = UtilsClass.LookAt(gameObject.transform.position, Unit.Target.position);
            }
        }
    }
}
