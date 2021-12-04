using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace TowerDeffence
{

    

    

    public class Tower : MonoBehaviour
    {
        public TowerType TowerType;
        public Transform Mesh;
        public Transform MainActionRange;
        private List<BaseEnemyComponent> _enemies;
        private List<ActionBase> _towers;
        private List<Transform> _waypoints;

        public List<ActionBase> ThisActionComponents;
        private List<string> buffsForRemove = new List<string>();
        private int[] _maxUpgrades = new int[] { 5, 5, 5 };
        private int[] _currentUpgrades = new int[] { 0, 0, 0 };

        private void Update()
        {
            foreach(var actionComponent in ThisActionComponents)
            {
                ActionComponentBuffsHandle(actionComponent);
                ActionComponentCooldownHandle(actionComponent);
            }
        }


        private void ActionComponentCooldownHandle(ActionBase actionComponent)
        {
            if (!actionComponent.enabled) return;
            actionComponent.CurrentActionCooldown -= Time.deltaTime;
            if (actionComponent.CurrentActionCooldown <= 0)
                if (actionComponent.PerformAction())
                {
                    Helper.ExpManager.AddExp(TowerType);
                    Helper.PanelController.UpdatePanel(TowerType);
                    float buffModifier = 1;
                    foreach (var buff in actionComponent.Buffs.Where(t => t.Value.ActionSpeed > 0))
                    {
                        buffModifier *= buff.Value.ActionSpeed + 1;
                    }
                    actionComponent.CurrentActionCooldown = 1 / (actionComponent.ActionsPerSecond * buffModifier);
                }
        }

        private void ActionComponentBuffsHandle(ActionBase actionComponent)
        {            
            foreach(var buff in actionComponent.Buffs)
            {
                var currentBuff = buff.Value;
                currentBuff.Time -= Time.deltaTime;
                if(currentBuff.Time <= 0)
                {
                    buffsForRemove.Add(buff.Key);                    
                }
            }
            foreach(var buff in buffsForRemove)
            {
                actionComponent.Buffs.Remove(buff);
            }
            buffsForRemove.Clear();
        }


        public void ConfigureTower(List<BaseEnemyComponent> enemies, List<ActionBase> towers, List<Transform> wayPoints)
        {
            
            _enemies = enemies;
            _towers = towers;
            _waypoints = wayPoints; 
            foreach(var actionComponent in ThisActionComponents)
            {
                if (actionComponent is ActionNotargetAttack) ((ActionNotargetAttack)actionComponent).Enemies = _enemies;
                if (actionComponent is ActionTargetAttack) ((ActionTargetAttack)actionComponent).Enemies = _enemies;
                if (actionComponent is ActionUniqueSniper) ((ActionUniqueSniper)actionComponent).Enemies = _enemies;
                if (actionComponent is ActionBuff) ((ActionBuff)actionComponent).Towers = _towers;
                if (actionComponent is ActionWaypathTrap) ((ActionWaypathTrap)actionComponent).Waypoints = _waypoints;
            }
        }

        public int[] GetMaxUpgrades()
        {
            return _maxUpgrades;
        }

        public int[] GetCurrentUpgrades()
        {
            return _currentUpgrades;
        }

    }

}





/*
           switch (id)
           {
               case 1:

                   break;
               case 2:

                   break;
               case 3:

                   break;
               case 4:

                   break;
               case 5:

                   break;
               case 6:

                   break;
               case 7:

                   break;
               case 8:

                   break;
               case 9:

                   break;
               case 10:

                   break;
               case 11:

                   break;
               case 12:

                   break;
               case 13:

                   break;
               case 14:

                   break;
               case 15:

                   break;

           }
           */