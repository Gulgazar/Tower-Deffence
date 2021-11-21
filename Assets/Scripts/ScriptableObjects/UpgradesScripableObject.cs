using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "TowerUpgrades", menuName = "Towers/UpgradesTowerConfig")]
    public class UpgradesScripableObject : ScriptableObject
    {
        public TowerType TowerType;
        public List<UpgradeData> FisrtRow = new List<UpgradeData>();
        public List<UpgradeData> SecondRow = new List<UpgradeData>();
        public List<UpgradeData> ThirdRow = new List<UpgradeData>();
        

        private UpgradesScripableObject()
        {
            for(int i = 1; i <= 5; i++)
            {
                FisrtRow.Add(new UpgradeData { ID = string.Format("ROW_FIRST_UPGRADE_{0}", i) });
            }
            for (int i = 1; i <= 5; i++)
            {
                SecondRow.Add(new UpgradeData { ID = string.Format("ROW_SECOND_UPGRADE_{0}", i) });
            }
            for (int i = 1; i <= 5; i++)
            {
                ThirdRow.Add(new UpgradeData { ID = string.Format("ROW_THIRD_UPGRADE_{0}", i) });
            }
        }


    }

}