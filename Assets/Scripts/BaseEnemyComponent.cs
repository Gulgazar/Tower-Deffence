using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDeffence
{
    public class BaseEnemyComponent : MonoBehaviour
    {
        public Transform MeshGameObject;
        public EnemyType Type;
        public EnemyType[] Children;
        public bool IsBoss;
        public int Health;
        public int NextPoint = 0;
        public float SpeedModifier;
        public float DistanceToNextPoint = 0;
        public bool Lead;
        public bool Camo;        
        public int LastWave;

        public void GetDamage(ActionAttack sourceTower, BaseProjectileComponent source = null)
        {

            Health -= sourceTower.Damage;
            if (Lead) Health -= sourceTower.AdditionalLeadDamage;
            if (IsBoss) Health -= sourceTower.AdditionalBossDamage;
            if (Health == 0 || (Health < 0 && IsBoss))
            {
                Helper.EnemyHandler.KillEnemy(this, 0, source);
            }
            else if (Health < 0)
            {
                int excessiveDamage = -Health;
                Helper.EnemyHandler.KillEnemy(this, excessiveDamage, source);
            }
        }



    }

}
