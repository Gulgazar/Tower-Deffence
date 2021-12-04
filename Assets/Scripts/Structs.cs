using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{

    [Serializable]
    public class TowerData
    {
        public TowerType TowerType;
        public string Name;
        public int Cost;
        public Sprite Image;
    }




    [Serializable]
    public class UpgradeData
    {
        public string ID;
        public string Name;
        public string Description;
        public int Cost;
    }

    public static class Helper
    {
        public const string EnemyTag = "Enemy";
        public const float BaseSpeed = 200f;
        public static IProjectileHandler ProjectileHandler { get; set; }
        public static IEnemyHandler EnemyHandler { get; set; }
        public static ExpManager ExpManager { get; set; }
        public static IPanelController PanelController { get; set; }

        public static MapType MapType = MapType.Map_Grassfields;
        public static Difficulty Difficulty;
        private static Dictionary<Difficulty, int> TotalWaves = new Dictionary<Difficulty, int>()
        {
            
            {Difficulty.Easy, 20 },
            {Difficulty.Normal, 30 },
            {Difficulty.Hard, 40 },
            {Difficulty.TestOnly, 2 }
        };

        private static Dictionary<Difficulty, float> CostModidier = new Dictionary<Difficulty, float>()
        { 
            { Difficulty.Easy, 0.9f }, 
            { Difficulty.Normal, 1f }, 
            { Difficulty.Hard, 1.1f },
            {Difficulty.TestOnly, 0f }
        };

        public static float GetCostModifier()
        {
            return CostModidier[Difficulty];
        }
        public static int GetTotalWaves()
        {
            return TotalWaves[Difficulty];
        }


    }


    [Serializable]
    public class EnemyTypeControllerDictionary : SerializableDictionary<EnemyType, BaseEnemyComponent> { }
    [Serializable]
    public class ProjectileTypeControllerDictionary : SerializableDictionary<ProjectileType, BaseProjectileComponent> { }
    [Serializable]
    public class TowerTypePrefabDictionary : SerializableDictionary<TowerType, Tower> { }


    [Serializable]
    public struct EnemyData
    {
        public EnemyType EnemyType;
        public EnemyType[] Children;
        public int Health;
        public bool Lead;
        public bool Camo;
        public bool IsBoss;
        public float SpeedModifier;
        [Space]
        public Material Material;
    }
    [Serializable]
    public struct Wave
    {
        public int WaveNumber;
        public List<WaveStage> Stages;
    }

    [Serializable]
    public struct WaveStage
    {
        public bool Camo;
        public EnemyType EnemyType;
        public int EnemyCount;
        public float Cooldown;
        public float CooldownToNextStage;
    }

    public struct ProjectileData
    {
        public ProjectileType ProjectileType;
        public ProjectileType[] Children;
    }

    public interface IEnemyHandler
    {
        void KillEnemy(BaseEnemyComponent enemy, int excessiveDamage = 0, BaseProjectileComponent source = null);
        BaseEnemyComponent SpawnEnemy(EnemyType enemyType, Vector3 position, int nextPoint, float distanceToNextWaypoint, bool camo);
    }

    public interface IProjectileHandler
    {
        void CreateProjectile(ActionAttack source);
        void DestroyProjectile(BaseProjectileComponent projectile);
    }
    public interface IPanelController
    {
        void UpdatePanel(TowerType towerType);
    }

    [Serializable]
    public struct BuffGroup
    {
        public string ID;
        public List<Buff> Buffs;
    }

    [Serializable]
    public class Buff : ICloneable
    {
        [NonSerialized]
        public string ID;
        public int Level;        
        public float Time;
        public bool CanSeeCamo;
        public bool CanBreakLead;
        public float ActionSpeed;
        public float ActionRange;
        public float Damage;
        public List<ActionType> ActionComponentsAffection;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}