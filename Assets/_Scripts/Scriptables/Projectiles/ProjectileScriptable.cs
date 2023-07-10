using System;
using UnityEngine;

namespace Assets.Scriptables.Units
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
    public class ProjectileScriptable : UnitScriptable
    {
        public ProjectileType projectileType;
    }

    public enum ProjectileType
    {
        Bullet = 0,
    }
}
