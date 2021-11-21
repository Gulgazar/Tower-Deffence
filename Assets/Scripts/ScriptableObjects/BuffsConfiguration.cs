using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "Buffs", menuName = "Buffs/BuffsConfiguration")]
    public class BuffsConfiguration : ScriptableObject
    {
        public List<BuffGroup> Buffs;
    }

}