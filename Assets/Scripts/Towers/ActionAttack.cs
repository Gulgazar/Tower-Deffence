using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public abstract class ActionAttack : ActionBase
    {

        public Transform AttackPoint;
        public List<BaseEnemyComponent> Enemies;
        public bool CanSeeCamo;
        public bool CanBreakLead;
        public int Damage;
        public int AdditionalLeadDamage;
        public int AdditionalBossDamage;
        public int MaxTargets;
        public int ProjectilesCount;
        public float ExlosionRange;
        public float ProjectileRange;
        public float ProjectileSpeed;
        public ProjectileType ProjectileType;

        protected Quaternion _defaultAttackPointRotation;

        public override abstract bool PerformAction();

        private void Awake()
        {
            _defaultAttackPointRotation = AttackPoint.localRotation;
        }
    }
}
