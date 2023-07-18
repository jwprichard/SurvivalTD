using UnityEngine;
using Assets.Scriptables.Units;
using System.Collections.Generic;
using System;

namespace Assets.Units
{
    public class BuildingCombatController : CombatController
    {
        private ProjectileScriptable Ammunition;

        public override void Attack()
        {
            //if (Unit.Target == null) return;
            CreateAmmo();
        }

        private GameObject CreateAmmo()
        {
            GameObject projectile = Instantiate(Ammunition.prefab);
            projectile.transform.position = AttackPoint.position;
            projectile.GetComponent<Unit>().SetTarget(Unit.Target);
            return projectile;
        }

        public void SetAmmo(ProjectileScriptable Ammo)
        {
            Ammunition = Ammo;
        }
    }
}
