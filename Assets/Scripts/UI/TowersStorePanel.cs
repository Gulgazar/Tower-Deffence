using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDeffence
{
    public class TowersStorePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private PlayerControlsComponent _controls;
        private int _layerMaskUI;

        



        public void Awake()
        {
            _layerMaskUI = LayerMask.GetMask("UI");
            
        }

        //Самому смешно, но времени исправлять нет))
        public TowerData GetTowerData(TowerType type)
        {
            return _controls.GetTowerData(type);
        }

        public void SelectTower(TowerType type)
        {            
            _controls.SelectTower(type);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            _controls.PointerOnUI = true;
            if(_controls.IsDragTower)
            {
                print("UNBUILD");
                _controls.UndoInstantiateTower();
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            _controls.PointerOnUI = false;
            if (_controls.ReadyToBuild)
            {
                print("BUILD ");
                _controls.PreInstantiateTower();
            }
        }
    }
}
