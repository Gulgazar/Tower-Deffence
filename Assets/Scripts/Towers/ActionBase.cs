using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public abstract class ActionBase : MonoBehaviour
    {
        /*
        public TowerType TowerType;
        public bool CanBreakLead;
        public bool CanSeeCamo;
        public int Damage;
        public int MaxTargets;
        public float BaseMainActionCooldown;
        public float CurrentMainActionCooldown;
        public bool HasSecondaryAction;
        public float BaseSecondaryActionCooldown;
        public float CurrentSecondaryActionCooldown;
        public float AttackRange;
        public float ProjectileRange;
        public float ProjectileSpeed;
        public TargetType targetType;
        public SingleTargetPriority TargetPriority;        
        public BaseProjectileComponent BaseProjectile;

        public abstract bool PerformMainAction(MonoBehaviour target, out BaseProjectileComponent projectile);       
        public abstract bool PerformSecondaryAction(MonoBehaviour target, out BaseProjectileComponent projectile);
        */

        //public TowerType TowerType;
        public float ActionsPerSecond;
        public float CurrentActionCooldown;
        public bool Main;
        public float ActionRange;
        public ActionType ActionType;

        public Dictionary<string, Buff> Buffs = new Dictionary<string, Buff>();

        public abstract bool PerformAction();
        

    }

}