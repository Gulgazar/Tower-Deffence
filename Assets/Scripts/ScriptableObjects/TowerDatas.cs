using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "TowerDatas", menuName = "Towers/BaseTowerConfig")]
    public class TowerDatas : ScriptableObject
    {
        public List<TowerData> BaseTowerDatas;
    }
}