using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public abstract class ActionBase : MonoBehaviour
    {

        public float ActionsPerSecond;
        public float CurrentActionCooldown;
        public bool Main;
        public float ActionRange;
        public ActionType ActionType;

        public Dictionary<string, Buff> Buffs = new Dictionary<string, Buff>();

        public abstract bool PerformAction();
        

    }

}