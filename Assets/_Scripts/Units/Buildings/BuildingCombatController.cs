using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables.Units;

namespace Assets.Units
{
    public class BuildingCombatController : BaseCombatController
    {
        private AmmunitionScriptableBase Ammunition;

        public void Start()
        {
            Timer.Init(1, true);
            Timer.OnTimerElapsed += (s, e) => { Attack(); };
        }

        public override void Attack()
        {
            GameObject projectile = CreateAmmo();

        }

        private GameObject CreateAmmo()
        {
            GameObject projectile = Instantiate(Ammunition.prefab);
            projectile.transform.position = AttackPoint.position;
            projectile.GetComponent<UnitBase>().SetTarget(Unit.Target);
            return projectile;
        }

        public void SetAmmo(AmmunitionScriptableBase Ammo)
        {
            Ammunition = Ammo;
        }
    }
}
