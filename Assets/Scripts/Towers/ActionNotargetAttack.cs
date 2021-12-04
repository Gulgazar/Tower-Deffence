using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ActionNotargetAttack : ActionAttack
    {
        public float ProjectileDegree;
        public override bool PerformAction()
        {

            if (FindTasrgetsInRange())
            {


                for(int i = 0; i < ProjectilesCount; i++)
                {
                    AttackPoint.Rotate(0, i * ProjectileDegree, 0);
                    Helper.ProjectileHandler.CreateProjectile(this);
                    AttackPoint.localRotation = _defaultAttackPointRotation;
                }

                return true;
            }
            return false;
        }

        private bool FindTasrgetsInRange()
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                if ((transform.position - Enemies[i].transform.position).sqrMagnitude <= Mathf.Pow(ActionRange, 2))
                {
                    return true;
                }
            }
            return false;
        }
    }

}