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
        eye=0,
        average = 1,
        tanky = 2,
    }
}
