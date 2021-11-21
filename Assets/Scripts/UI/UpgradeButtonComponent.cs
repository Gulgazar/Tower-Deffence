using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TowerDeffence
{
    public class UpgradeButtonComponent : MonoBehaviour
    {
        public UpgradeRowType UpgradeRowType;

        [Space(10)]
        
        public Toggle[] ToggleUpgrades;
        [NonSerialized]
        public Text AvailableUpgradeText;
        [NonSerialized]
        public TextMeshPro CurrentUpgradeText;
        [NonSerialized]
        public Button ThisButton;
        [NonSerialized]
        public Text Text;
        [SerializeField]
        private PlayerControlsComponent _controls;
        
        

        private int _currentCost;
        

        private void Awake()
        {
            ThisButton = GetComponent<Button>();
            Text = GetComponentInChildren<Text>();
        }

        public void PerformUpgrade_UnityEditor()
        {
            _controls.BuyUpgrade(UpgradeRowType);
        }


    }

}