using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class AttackSniperTowerComponent : ActionAttack
    {
        public override bool PerformAction()
        {
            return false;
        }

        private bool DefineTowerTarget(out BaseEnemyComponent enemy)
        {
            throw new System.NotImplementedException();
        }
    }

}