using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDeffence
{
    public class TowerButtonComponent : MonoBehaviour
    {
        [SerializeField]
        private TowerType _towerType;
        [SerializeField]
        private TowersStorePanel _store;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private Image _image;
        

        public void SelectTower_UnityEditor()
        {
            _store.SelectTower(_towerType);
        }

        private void Start()
        {
            var data = _store.GetTowerData(_towerType);
            _text.text = Mathf.RoundToInt(data.Cost * Helper.GetCostModifier()) + "$";
            _image.sprite = data.Image;
        }
    }

}