using UnityEngine;

namespace Assets.Scriptables.Units
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
    public class EnemyScriptable : UnitScriptable
    {
        public EnemyType enemyType;
        public int SpawnCost;
    }

    public enum EnemyType
    {
        Eye=3,
        Average = 5,
        Tanky = 15,
    }
}
