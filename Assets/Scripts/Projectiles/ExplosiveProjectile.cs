using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ExplosiveProjectile : BaseProjectileComponent
    {
        //public float ExplosionRange;
        private List<BaseEnemyComponent> _enemiesCurrentState = new List<BaseEnemyComponent>();
        public override void ConfigureProjectile(ActionAttack source)
        {
            //Speed = source.ProjectileSpeed;


            //Damage = source.Damage;

            //CanBreakLead = source.CanBreakLead;
            //CanSeeCamo = source.CanSeeCamo;
            //ExplosionRange = source.ExlosionRange;

            SourceTower = source;
            LifeTime = source.ProjectileRange / source.ProjectileSpeed;
            TargetsLeft = source.MaxTargets;
            _enemiesCurrentState.Clear();
            _enemiesCurrentState.AddRange(source.Enemies);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != Helper.EnemyTag) return;
            var enemy = other.GetComponent<BaseEnemyComponent>();
            if (_affectedEnemies.Contains(enemy)) return;
            if (enemy.Camo && !SourceTower.CanSeeCamo) return;
            if (enemy.Lead && !SourceTower.CanBreakLead)
            {
                //Попадание по свинцу без возможность пробтия автоматически уничтожает снаряд
                TargetsLeft = 0;
                DestroyProjectile();
                return;
            }
            _affectedEnemies.Add(enemy);
            enemy.GetDamage(SourceTower);
            TargetsLeft--;
            foreach (var en in _enemiesCurrentState)
            {
                if (en == enemy) continue;
                if (Vector3.SqrMagnitude(en.transform.position - transform.position) > Mathf.Pow(SourceTower.ExlosionRange, 2)) continue;
                if (en.Camo) continue;
                if (en.Lead && !SourceTower.CanBreakLead)
                {
                    TargetsLeft--;
                    continue;
                }
                en.GetDamage(SourceTower);
                TargetsLeft--;
                if(TargetsLeft <= 0) break;
            }
            DestroyProjectile();
        }
    }

}