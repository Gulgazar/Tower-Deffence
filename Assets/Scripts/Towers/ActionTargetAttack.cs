using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ActionTargetAttack : ActionAttack
    {
        public SingleTargetPriority TargetPriority;
        public float ProjectileDegree;
        public override bool PerformAction()
        {
            if(DefineSingleTowerTarget(out var enemy))
            {

                transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));

                int pCount = (ProjectilesCount - 1) / 2;
                for(int i = -pCount; i <= pCount; i++)
                {
                    AttackPoint.Rotate(0, i * ProjectileDegree, 0);
                    Helper.ProjectileHandler.CreateProjectile(this);
                    AttackPoint.localRotation = _defaultAttackPointRotation;
                }
                
                return true;
            }
            return false;
        }

        

        protected virtual bool DefineSingleTowerTarget(out BaseEnemyComponent enemy)
        {

            enemy = default;
            if (Enemies.Count < 1)
            {
                return false;
            }

            var enemiesInRange = new List<BaseEnemyComponent>();
            for (int i = 0; i < Enemies.Count; i++)
            {
                if ((transform.position - Enemies[i].transform.position).sqrMagnitude <= Mathf.Pow(ActionRange, 2)) enemiesInRange.Add(Enemies[i]);
            }

            if (enemiesInRange.Count < 1)
            {
                return false;
            }

            switch (TargetPriority)
            {
                case SingleTargetPriority.First:
                    var lastPoint = 0;
                    var distanceToNextPoint = float.MaxValue;

                    for (int i = 0; i < enemiesInRange.Count; i++)
                    {
                        if (enemiesInRange[i].NextPoint > lastPoint)
                        {
                            lastPoint = enemiesInRange[i].NextPoint;
                            distanceToNextPoint = enemiesInRange[i].DistanceToNextPoint;
                            enemy = enemiesInRange[i];
                        }
                        else if (enemiesInRange[i].NextPoint == lastPoint)
                        {
                            if (enemiesInRange[i].DistanceToNextPoint < distanceToNextPoint)
                            {
                                distanceToNextPoint = enemiesInRange[i].DistanceToNextPoint;
                                enemy = enemiesInRange[i];
                            }
                        }
                    }
                    return true;
                case SingleTargetPriority.Nearest:
                    var sqrDistance = float.MaxValue;
                    for (int i = 0; i < enemiesInRange.Count; i++)
                    {
                        var sqrMagnitude = (transform.position - Enemies[i].transform.position).sqrMagnitude;
                        if (sqrMagnitude < sqrDistance)
                        {
                            sqrDistance = sqrMagnitude;
                            enemy = enemiesInRange[i];
                        }
                    }
                    return true;
                case SingleTargetPriority.Strong:
                    var health = 0;
                    for (int i = 0; i < enemiesInRange.Count; i++)
                    {
                        if (enemiesInRange[i].Health > health)
                        {
                            health = enemiesInRange[i].Health;
                            enemy = enemiesInRange[i];
                        }
                    }
                    return true;
                default:
                    return false;
            }

        }
    }

}