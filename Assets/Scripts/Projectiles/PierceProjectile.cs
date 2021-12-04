using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class PierceProjectile : BaseProjectileComponent
    {
        public override void ConfigureProjectile(ActionAttack source)
        {

            SourceTower = source;
            LifeTime = source.ProjectileRange / source.ProjectileSpeed;
            TargetsLeft = source.MaxTargets;
            _affectedEnemies.Clear();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != Helper.EnemyTag) return;
            var enemy = other.GetComponent<BaseEnemyComponent>();
            if (_affectedEnemies.Contains(enemy)) return;
            if (enemy.Camo && !SourceTower.CanSeeCamo) return;
            if (enemy.Lead && !SourceTower.CanBreakLead)
            {
                TargetsLeft = 0;
                DestroyProjectile();
                return;
            }
            _affectedEnemies.Add(enemy);
            enemy.GetDamage(SourceTower, this);
            TargetsLeft--;
            if (TargetsLeft <= 0)
            {
                DestroyProjectile();
            }
        }

    }

}