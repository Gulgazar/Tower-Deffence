using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class Upgrades
    {

        // Да, я понимаю всю проблему свича на 150 вариантов. Да, я пытался решить её. 2 недели.
        // Но кастомный редактор пока для меня слишком сложно (вернее, уже не зхватит времени), а другие варианты как я ни пытался, не получилось реализовать.
        // Пришлось делать по принципу "Лишь бы работало".
        private Dictionary<string, Buff> _buffs = new Dictionary<string, Buff>();

        public Upgrades()
        {
            if (!Resources.Load<BuffsConfiguration>("BuffsConfiguration"))
            {
                Debug.LogError("There something wrong with /Resourses/Buffs");
                return;
            }
            foreach(var buffType in Resources.Load<BuffsConfiguration>("BuffsConfiguration").Buffs)
            {
                for(int i = 0; i < buffType.Buffs.Count; i++)
                {
                    var buff = buffType.Buffs[i];
                    buff.ID = buffType.ID + "_" + buff.Level;
                    _buffs.Add((buff.ID), buff);
                }
                /*
                foreach(var buffLevel in buffType.Buffs)
                {
                    buffLevel.ID = buffType.ID + "_" + buffLevel.Level;
                    _buffs.Add((), buffLevel);
                    //Debug.Log(buffLevel.Level +""+ buffType.ID);
                }
                */
            }
        }


        public void PerformUpgrade(Tower tower, string ID)
        {
            

            var notargetAttack = tower.GetComponent<ActionNotargetAttack>();
            var waypathTrap = tower.GetComponent<ActionWaypathTrap>();

            

            switch (tower.TowerType)
            {
                case TowerType.DartTower:
                    Upgrade_DartTower(tower, ID);
                    Debug.Log(ID + " Performed to " + tower);
                    break;
                case TowerType.BombShooter:
                    Upgrade_BombShooter(tower, ID);
                    Debug.Log(ID + " Performed to " + tower);
                    break;
                case TowerType.Spiker:
                    Upgrade_Spiker(tower, ID);
                    Debug.Log(ID + " Performed to " + tower);
                    break;
                case TowerType.SniperTower:

                    break;
                case TowerType.AceTower:

                    break;
                case TowerType.Alchemist:
                    Upgrade_Alchemist(tower, ID);
                    Debug.Log(ID + " Performed to " + tower);
                    break;
                case TowerType.Farm:

                    break;
                case TowerType.Pylon:

                    break;
                case TowerType.SpikeFactory:

                    break;

            }
        } 
        private void Upgrade_Spiker(Tower tower, string ID)
        {
            var targetAttack = tower.GetComponent<ActionTargetAttack>();
            Debug.Log(tower + "  " + ID);
            switch (ID)
            {
                //ROW 1
                case "SPIKER_ROW_FIRST_UPGRADE_1":
                    targetAttack.ExlosionRange *= 1.5f;
                    targetAttack.MaxTargets += 6;
                    break;
                case "SPIKER_ROW_FIRST_UPGRADE_2":
                    targetAttack.MaxTargets += 6;
                    targetAttack.Damage += 1;
                    break;
                case "SPIKER_ROW_FIRST_UPGRADE_3":
                    targetAttack.ExlosionRange *= 1.5f;
                    targetAttack.Damage += 1;
                    targetAttack.MaxTargets += 20;
                    break;
                case "SPIKER_ROW_FIRST_UPGRADE_4":
                    //DEBUFF?
                    targetAttack.ProjectileRange *= 1.3f;
                    break;
                case "SPIKER_ROW_FIRST_UPGRADE_5":
                    //DEBUFF?
                    break;
                //ROW 2
                case "SPIKER_ROW_SECOND_UPGRADE_1":
                    targetAttack.ActionsPerSecond *= 1.3f;
                    break;
                case "SPIKER_ROW_SECOND_UPGRADE_2":
                    targetAttack.ProjectileType = ProjectileType.Missle;
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.ProjectileRange *= 1.4f;
                    targetAttack.ProjectileSpeed *= 1.5f;
                    break;
                case "SPIKER_ROW_SECOND_UPGRADE_3":
                    targetAttack.ProjectileRange *= 1.4f;
                    targetAttack.AdditionalBossDamage += 15;
                    targetAttack.AdditionalLeadDamage += 2;
                    break;
                case "SPIKER_ROW_SECOND_UPGRADE_4":
                    targetAttack.AdditionalBossDamage += 20;
                    targetAttack.AdditionalLeadDamage += 2;
                    targetAttack.ProjectileRange *= 1.2f;
                    break;
                case "SPIKER_ROW_SECOND_UPGRADE_5":
                    targetAttack.AdditionalBossDamage += 100;
                    break;
                //ROW 3
                case "SPIKER_ROW_THIRD_UPGRADE_1":
                    targetAttack.ProjectileRange *= 1.3f;
                    break;
                case "SPIKER_ROW_THIRD_UPGRADE_2":
                    targetAttack.Damage += 1;
                    targetAttack.ProjectilesCount += 1;
                    targetAttack.ProjectileDegree = 30f;
                    break;
                case "SPIKER_ROW_THIRD_UPGRADE_3":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.ProjectilesCount += 1;
                    targetAttack.ProjectileDegree = 20f;
                    break;
                case "SPIKER_ROW_THIRD_UPGRADE_4":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.Damage += 1;
                    break;
                case "SPIKER_ROW_THIRD_UPGRADE_5":
                    targetAttack.Damage += 2;
                    targetAttack.CanSeeCamo = true;
                    break;

            }
        }

        private void Upgrade_BombShooter(Tower tower, string ID)
        {
            var targetAttack = tower.GetComponent<ActionTargetAttack>();
            Debug.Log(tower + "  " + ID);
            switch (ID)
            {
                //ROW 1
                case "BOMBSHOOTER_ROW_FIRST_UPGRADE_1":
                    targetAttack.ExlosionRange *= 1.5f;
                    targetAttack.MaxTargets += 6;
                    break;
                case "BOMBSHOOTER_ROW_FIRST_UPGRADE_2":
                    targetAttack.MaxTargets += 6;
                    targetAttack.Damage += 1;
                    break;
                case "BOMBSHOOTER_ROW_FIRST_UPGRADE_3":
                    targetAttack.ExlosionRange *= 1.5f;
                    targetAttack.Damage += 1;
                    targetAttack.MaxTargets += 20;                    
                    break;
                case "BOMBSHOOTER_ROW_FIRST_UPGRADE_4":
                    //DEBUFF?
                    targetAttack.ProjectileRange *= 1.3f;                    
                    break;
                case "BOMBSHOOTER_ROW_FIRST_UPGRADE_5":
                    //DEBUFF?
                    break;
                //ROW 2
                case "BOMBSHOOTER_ROW_SECOND_UPGRADE_1":
                    targetAttack.ActionsPerSecond *= 1.3f;
                    break;
                case "BOMBSHOOTER_ROW_SECOND_UPGRADE_2":
                    targetAttack.ProjectileType = ProjectileType.Missle;
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.ProjectileRange *= 1.4f;
                    targetAttack.ProjectileSpeed *= 1.5f;
                    break;
                case "BOMBSHOOTER_ROW_SECOND_UPGRADE_3":
                    targetAttack.ProjectileRange *= 1.4f;
                    targetAttack.AdditionalBossDamage += 15;
                    targetAttack.AdditionalLeadDamage += 2;
                    break;
                case "BOMBSHOOTER_ROW_SECOND_UPGRADE_4":
                    targetAttack.AdditionalBossDamage += 20;
                    targetAttack.AdditionalLeadDamage += 2;
                    targetAttack.ProjectileRange *= 1.2f;
                    break;
                case "BOMBSHOOTER_ROW_SECOND_UPGRADE_5":
                    targetAttack.AdditionalBossDamage += 100;
                    break;
                //ROW 3
                case "BOMBSHOOTER_ROW_THIRD_UPGRADE_1":
                    targetAttack.ProjectileRange *= 1.3f;
                    break;
                case "BOMBSHOOTER_ROW_THIRD_UPGRADE_2":
                    targetAttack.Damage += 1;
                    targetAttack.ProjectilesCount += 1;
                    targetAttack.ProjectileDegree = 30f;
                    break;
                case "BOMBSHOOTER_ROW_THIRD_UPGRADE_3":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.ProjectilesCount += 1;
                    targetAttack.ProjectileDegree = 20f;
                    break;
                case "BOMBSHOOTER_ROW_THIRD_UPGRADE_4":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.Damage += 1;
                    break;
                case "BOMBSHOOTER_ROW_THIRD_UPGRADE_5":
                    targetAttack.Damage += 2;
                    targetAttack.CanSeeCamo = true;
                    break;

            }
        }

        private void Upgrade_DartTower(Tower tower, string ID)
        {
            var targetAttack = tower.GetComponent<ActionTargetAttack>();
            Debug.Log(tower + "  " + ID);
            switch (ID)
            {
                //ROW 1
                case "DARTTOWER_ROW_FIRST_UPGRADE_1":
                    targetAttack.MaxTargets += 1;
                    break;
                case "DARTTOWER_ROW_FIRST_UPGRADE_2":
                    targetAttack.MaxTargets += 2;
                    break;
                case "DARTTOWER_ROW_FIRST_UPGRADE_3":
                    targetAttack.ProjectileType = ProjectileType.SpikedBall;
                    targetAttack.MaxTargets += 20;
                    targetAttack.ActionsPerSecond /= 1.5f;
                    targetAttack.ProjectileRange *= 2;
                    targetAttack.ActionRange *= 2;
                    tower.MainActionRange.localScale = new Vector3(targetAttack.ActionRange / 5, 0, targetAttack.ActionRange / 5);
                    targetAttack.CanBreakLead = true;
                    break;
                case "DARTTOWER_ROW_FIRST_UPGRADE_4":
                    targetAttack.MaxTargets += 30;
                    targetAttack.Damage += 1;
                    targetAttack.ProjectileSpeed *= 1.3f;
                    targetAttack.ActionsPerSecond *= 1.2f;
                    targetAttack.AdditionalLeadDamage += 3;
                    break;
                case "DARTTOWER_ROW_FIRST_UPGRADE_5":
                    targetAttack.ProjectileType = ProjectileType.SpikedBallReinforced;
                    break;
                //ROW 2
                case "DARTTOWER_ROW_SECOND_UPGRADE_1":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    break;
                case "DARTTOWER_ROW_SECOND_UPGRADE_2":
                    targetAttack.ActionsPerSecond *= 1.3f;
                    break;
                case "DARTTOWER_ROW_SECOND_UPGRADE_3":
                    targetAttack.ProjectilesCount = 3;
                    targetAttack.ProjectileDegree = 20;
                    break;
                case "DARTTOWER_ROW_SECOND_UPGRADE_4":
                    targetAttack.ActionsPerSecond *= 3;
                    targetAttack.Damage += 2;
                    break;
                case "DARTTOWER_ROW_SECOND_UPGRADE_5":
                    targetAttack.ActionsPerSecond *= 4;
                    targetAttack.Damage += 3;
                    break;
                //ROW 3
                case "DARTTOWER_ROW_THIRD_UPGRADE_1":
                    targetAttack.ActionRange *= 1.5f;
                    tower.MainActionRange.localScale = new Vector3(targetAttack.ActionRange / 5, 0, targetAttack.ActionRange / 5);
                    targetAttack.ProjectileRange *= 1.5f;
                    break;
                case "DARTTOWER_ROW_THIRD_UPGRADE_2":
                    targetAttack.ActionRange *= 1.5f;
                    tower.MainActionRange.localScale = new Vector3(targetAttack.ActionRange / 5, 0, targetAttack.ActionRange / 5);
                    targetAttack.ProjectileRange *= 1.5f;
                    targetAttack.CanSeeCamo = true;
                    break;
                case "DARTTOWER_ROW_THIRD_UPGRADE_3":
                    targetAttack.ProjectileType = ProjectileType.Arrow;
                    targetAttack.ProjectileRange *= 2f;
                    targetAttack.Damage += 2;
                    break;
                case "DARTTOWER_ROW_THIRD_UPGRADE_4":
                    targetAttack.ActionsPerSecond *= 2;
                    targetAttack.Damage += 4;
                    break;
                case "DARTTOWER_ROW_THIRD_UPGRADE_5":
                    targetAttack.MaxTargets += 7;
                    targetAttack.ActionsPerSecond *= 3;
                    targetAttack.ProjectileRange *= 1.5f;
                    targetAttack.CanBreakLead = true;
                    break;

            }
        }

        private void Upgrade_Alchemist(Tower tower, string ID)
        {
            var targetAttack = tower.GetComponent<ActionTargetAttack>();
            var actionBuff = tower.GetComponent<ActionBuff>();
            Debug.Log(tower + "  " + ID);
            switch (ID)
            {
                //ROW 1
                case "ALCHEMIST_ROW_FIRST_UPGRADE_1":
                    targetAttack.ExlosionRange *= 1.2f;
                    targetAttack.MaxTargets += 5;
                    break;
                case "ALCHEMIST_ROW_FIRST_UPGRADE_2":
                    actionBuff.enabled = true;
                    actionBuff.CreateBuffRenderer();
                    actionBuff.Buff = _buffs["BUFF_ALCHEMIST_1"];
                    break;
                case "ALCHEMIST_ROW_FIRST_UPGRADE_3":
                    actionBuff.ActionsPerSecond *= 1.5f;
                    actionBuff.Buff = _buffs["BUFF_ALCHEMIST_2"];
                    break;
                case "ALCHEMIST_ROW_FIRST_UPGRADE_4":
                    actionBuff.Buff = _buffs["BUFF_ALCHEMIST_3"];
                    break;
                case "ALCHEMIST_ROW_FIRST_UPGRADE_5":
                    actionBuff.Buff = _buffs["BUFF_ALCHEMIST_4"];
                    break;
                //ROW 2
                case "ALCHEMIST_ROW_SECOND_UPGRADE_1":
                    targetAttack.Damage += 1;
                    break;
                case "ALCHEMIST_ROW_SECOND_UPGRADE_2":
                    targetAttack.Damage += 2;                    
                    break;
                case "ALCHEMIST_ROW_SECOND_UPGRADE_3":
                    targetAttack.Damage += 3;
                    break;
                case "ALCHEMIST_ROW_SECOND_UPGRADE_4":
                    targetAttack.Damage += 4;
                    break;
                case "ALCHEMIST_ROW_SECOND_UPGRADE_5":
                    targetAttack.Damage += 12;
                    break;
                //ROW 3
                case "ALCHEMIST_ROW_THIRD_UPGRADE_1":
                    targetAttack.ActionsPerSecond *= 1.2f;
                    break;
                case "ALCHEMIST_ROW_THIRD_UPGRADE_2":
                    targetAttack.MaxTargets += 10;
                    break;
                case "ALCHEMIST_ROW_THIRD_UPGRADE_3":
                    targetAttack.AdditionalLeadDamage += 3;
                    break;
                case "ALCHEMIST_ROW_THIRD_UPGRADE_4":
                    targetAttack.AdditionalBossDamage += 10;
                    break;
                case "ALCHEMIST_ROW_THIRD_UPGRADE_5":
                    targetAttack.ActionRange *= 2;
                    targetAttack.Damage += 4;
                    break;

            }
        }


    }
}
/*
switch (ID)
            {
                //ROW 1
                case "_ROW_FIRST_UPGRADE_1":

                    break;
                case "_ROW_FIRST_UPGRADE_2":

                    break;
                case "_ROW_FIRST_UPGRADE_3":

                    break;
                case "_ROW_FIRST_UPGRADE_4":

                    break;
                case "_ROW_FIRST_UPGRADE_5":

                    break;
                //ROW 2
                case "_ROW_SECOND_UPGRADE_1":

                    break;
                case "_ROW_SECOND_UPGRADE_2":

                    break;
                case "_ROW_SECOND_UPGRADE_3":

                    break;
                case "_ROW_SECOND_UPGRADE_4":

                    break;
                case "_ROW_SECOND_UPGRADE_5":

                    break;
                //ROW 3
                case "_ROW_THIRD_UPGRADE_1":

                    break;
                case "_ROW_THIRD_UPGRADE_2":

                    break;
                case "_ROW_THIRD_UPGRADE_3":

                    break;
                case "_ROW_THIRD_UPGRADE_4":

                    break;
                case "_ROW_THIRD_UPGRADE_5":

                    break;

            }
*/