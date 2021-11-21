using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TowerDeffence
{
    public class ActionBuff : ActionBase
    {
        
        public List<ActionBase> Towers;
        public Buff Buff;

        [SerializeField]
        private CapsuleCollider BuffVisualAffectionGameObject;

        private void Start()
        {

        }

        public override bool PerformAction()
        {
            
            ActionBase tower = null;
            foreach(var actionComponent in Towers)
            {
                if (!Buff.ActionComponentsAffection.Contains(actionComponent.ActionType)) continue;
                if (Vector3.SqrMagnitude(actionComponent.transform.position - transform.position) > Mathf.Pow(ActionRange, 2)) continue;
                if (actionComponent.Buffs.ContainsKey(Buff.ID))
                {
                    if (actionComponent.Buffs[Buff.ID].Level <= Buff.Level)
                    {
                        continue;
                    }
                    tower = actionComponent;                    
                }
                else
                {
                    actionComponent.Buffs.Add(Buff.ID, (Buff)Buff.Clone());
                    return true;
                }
            }
            if(tower != null)
            {
                tower.Buffs[Buff.ID] = (Buff)Buff.Clone();
                return true;
            }
            return false;
        }

        public void CreateBuffRenderer()
        {
            var buffRenderer = Instantiate(BuffVisualAffectionGameObject, transform);
            buffRenderer.transform.localPosition = Vector3.zero;
            buffRenderer.radius = ActionRange;
            buffRenderer.gameObject.layer = LayerMask.GetMask("Default");
        }

        
    }
}