using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ActionUniqueSniper : ActionAttack
    {
        public override bool PerformAction()
        {
            if (DefineSingleTowerTarget(out BaseEnemyComponent enemy))
            {
                enemy.GetDamage(this);
            }
            return false;
        }

        private bool DefineSingleTowerTarget(out BaseEnemyComponent enemy)
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
                        if (sqrMagnitude > sqrDistance)
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