using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public enum BaseEnemyColor
    {
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Pink = 5
    }

    public enum SingleTargetPriority
    {
        First = 0,
        Nearest = 1,
        Strong = 2,
    }

    public enum TargetType
    {
        SingleTarget = 1,
        TargetsInRange = 2,
        SinhleAlly = 3,
        AlliesInRange = 4,
    }

    public enum MapType
    {
        Map_Grassfields,
        Map_Winterlake
    }

    public enum Difficulty
    {
        
        Easy,
        Normal,
        Hard,
        TestOnly
    }

    public enum UpgradeRowType
    {
        First = 0,
        Second = 1,
        Third = 2,
    }

    public enum EnemyType 
    {
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Pink = 5,
        Lead = 6,
        A_Boss = 7,
        B_Boss = 8
    }

    public enum ProjectileType
    {
        Dart,
        Bomb,
        SpikedBall,
        SpikedBallReinforced,
        Arrow,
        Missle,
        Potion,

    }
    public enum ActionType
    {
        ActionTargetAttack,
        ActionNotargetAttack,
        ActionBuff,
        ActionTrap,
    }

    public enum TowerType
    {
        DartTower = 1,
        BombShooter = 2,
        Spiker = 3,
        SniperTower = 4,
        AceTower = 5,
        Alchemist = 6,
        Farm = 7,
        Pylon = 8,
        SpikeFactory = 9
    }
}