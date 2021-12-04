using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    
    public class ActionUniqueSniper : ActionTargetAttack
    {

        public float ReboundRange;
        public override bool PerformAction()
        {
            List<BaseEnemyComponent> affectedEnemies = new List<BaseEnemyComponent>();
            if (DefineSingleTowerTarget(out BaseEnemyComponent enemy))
            {
                print(enemy);
                transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));

                enemy.GetDamage(this);
                affectedEnemies.Add(enemy);
                DamageNextTarget(enemy.transform.position, affectedEnemies, MaxTargets - 1);
                return true;
            }

            return false;
        }

        private void DamageNextTarget(Vector3 previousTargetPosition, List<BaseEnemyComponent> affectedEnemies, int targetsLeft)
        {
            if (targetsLeft <= 0) return;
            for(int i = 0; i < Enemies.Count; i++)
            {
                var enemy = Enemies[i];
                if (affectedEnemies.Contains(enemy)) continue;
                if ((enemy.transform.position - previousTargetPosition).sqrMagnitude > Mathf.Pow(ReboundRange, 2)) return;
                enemy.GetDamage(this);
                affectedEnemies.Add(enemy);
                targetsLeft--;
                DamageNextTarget(enemy.transform.position, affectedEnemies, MaxTargets - 1);
            }
        }

        protected override bool DefineSingleTowerTarget(out BaseEnemyComponent enemy)
        {
            enemy = default;
            if (Enemies.Count < 1)
            {
                return false;
            }

            switch (TargetPriority)
            {
                case SingleTargetPriority.First:
                    var lastPoint = 0;
                    var distanceToNextPoint = float.MaxValue;

                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        if (Enemies[i].NextPoint > lastPoint)
                        {
                            lastPoint = Enemies[i].NextPoint;
                            distanceToNextPoint = Enemies[i].DistanceToNextPoint;
                            enemy = Enemies[i];
                        }
                        else if (Enemies[i].NextPoint == lastPoint)
                        {
                            if (Enemies[i].DistanceToNextPoint < distanceToNextPoint)
                            {
                                distanceToNextPoint = Enemies[i].DistanceToNextPoint;
                                enemy = Enemies[i];
                            }
                        }
                    }
                    return true;
                case SingleTargetPriority.Nearest:
                    var sqrDistance = float.MaxValue;
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        var sqrMagnitude = (transform.position - Enemies[i].transform.position).sqrMagnitude;
                        if (sqrMagnitude < sqrDistance)
                        {
                            sqrDistance = sqrMagnitude;
                            enemy = Enemies[i];
                        }
                    }
                    return true;
                case SingleTargetPriority.Strong:
                    var health = 0;
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        if (Enemies[i].Health > health)
                        {
                            health = Enemies[i].Health;
                            enemy = Enemies[i];
                        }
                    }
                    return true;
                default:
                    return false;
            }


        }
    }
}