using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class Enemy : Unit
    {
        [Header("Broadcasting To")]
        [SerializeField] private EnemyDeathEventChannelSO _onEnemyDeath = default;

        public new EnemyScriptable Scriptable => (EnemyScriptable)base.Scriptable;

        public override void OnDestroy()
        {
            base.OnDestroy();
            _onEnemyDeath.RaiseEvent(this);
        }
    }
}
