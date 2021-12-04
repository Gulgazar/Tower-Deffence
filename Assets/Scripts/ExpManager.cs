using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ExpManager : MonoBehaviour
    {

        private Dictionary<TowerType, LevelData> LevelDatas = new Dictionary<TowerType, LevelData>();
        private Dictionary<int, int> ExpToNextLevel = new Dictionary<int, int>()
        {
            {0, 20 },
            {1, 35 },
            {2, 50 },
            {3, 65 },
            {4, 80 }
        };

        private void ResetExpToZero()
        {
            foreach (TowerType type in Enum.GetValues(typeof(TowerType)))
            {
                PlayerPrefs.SetInt(type.ToString() + "Level", 0);
                PlayerPrefs.SetInt(type.ToString() + "Exp", 0);
            }
        }

        private class LevelData
        {
            public int Level;
            public int Exp;
        }

        private void Awake()
        {
            Helper.ExpManager = this;
            foreach (TowerType type in Enum.GetValues(typeof(TowerType)))
            {
                LevelDatas.Add(type, new LevelData() { Level = PlayerPrefs.GetInt(type.ToString() + "Level", 0), Exp = PlayerPrefs.GetInt(type.ToString() + "Exp", 0) });
            }

        }

        public void AddExp(TowerType towerType, int exp = 1)
        {
            var data = LevelDatas[towerType];
            data.Exp += exp;
            if(data.Level < 5 && data.Exp >= ExpToNextLevel[data.Level])
            {
                data.Exp -= ExpToNextLevel[data.Level];
                data.Level++;
            }
        }

        public int GetLevel(TowerType towerType)
        {
            return LevelDatas[towerType].Level;
        }

        public int GetExp(TowerType towerType)
        {
            return LevelDatas[towerType].Exp;
        }

        public bool GetExpToLevelup(TowerType towerType, out int exp)
        {
            if (!ExpToNextLevel.ContainsKey(LevelDatas[towerType].Level))
            {
                exp = default;
                return false;
            }
            exp = ExpToNextLevel[LevelDatas[towerType].Level];
            return true;
        }

        private void OnDisable()
        {
            foreach(var type in LevelDatas)
            {
                PlayerPrefs.SetInt(type.Key.ToString() + "Level", type.Value.Level);
                PlayerPrefs.SetInt(type.Key.ToString() + "Exp", type.Value.Exp);
            }
        }
    }

}