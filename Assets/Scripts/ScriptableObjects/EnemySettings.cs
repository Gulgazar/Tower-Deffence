using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "Enemies")]
    public class EnemySettings : ScriptableObject
    {
        public List<EnemyData> Enemies;
    }

}