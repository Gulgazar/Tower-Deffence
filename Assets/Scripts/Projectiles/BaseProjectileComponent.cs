using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public abstract class BaseProjectileComponent : MonoBehaviour
    {


        public ActionAttack SourceTower;
        protected List<BaseEnemyComponent> _affectedEnemies = new List<BaseEnemyComponent>();
        public float TargetsLeft;
        public float LifeTime = 1;
        public ProjectileType ProjectileType;
        public ProjectileType ChildrenType;
        public int ChildreCount;


        public abstract void ConfigureProjectile(ActionAttack source);

        

        protected void DestroyProjectile()
        {
            Helper.ProjectileHandler.DestroyProjectile(this);
        }

        public void AddAffectedChildren(BaseEnemyComponent enemy)
        {
            _affectedEnemies.Add(enemy);
        }

        public void ClearEnemiesList()
        {
            _affectedEnemies.Clear();
        }

    }

}