using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDeffence
{    public class UpgradePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private PlayerControlsComponent _controls;
        
        private int _layerMaskUI;
        private Text _towerName;

        public bool Open;



        public void Awake()
        {
            _layerMaskUI = LayerMask.GetMask("UI");
        }


        public void OnPointerEnter(PointerEventData data)
        {
            _controls.PointerOnUI = true;            
        }

        public void OnPointerExit(PointerEventData data)
        {
            _controls.PointerOnUI = false;
            
        }



        /*
        public void Call(UpgradeData[] _datas)
        {

            for (int i = 0; i < _buttons.Length; i++)
            {
                int b = i;
                _buttons[i].onClick.AddListener(() => UpRow(b));
                _buttons[i].interactable = _datas[i].Current != _datas[i].Max;
            }
        }

        private void UpRow(int i)
        {
            Debug.Log(i);
        }
        */

    }
}
