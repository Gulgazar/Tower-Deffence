using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

using static UnityEngine.InputSystem.InputAction;
using UnityEngine.EventSystems;
using TMPro;

namespace TowerDeffence
{
    public class PlayerControlsComponent : MonoBehaviour, IPanelController
    {
        [SerializeField]
        private GameManager _gameManager;
        
        [SerializeField]
        private UpgradePanel _upgradePanelLeft;
        [SerializeField]
        private UpgradePanel _upgradePanelRight;
        [SerializeField]
        private UpgradeButtonComponent[] _leftButtons;
        [SerializeField]
        private UpgradeButtonComponent[] _rightButtons;
        [SerializeField]
        private Text[] _leftButtonsText;
        [SerializeField]
        private Text[] _rightButtonsText;
        [SerializeField]
        private string UnableToUpgradeText = "Недоступно";
        [SerializeField]
        private TowerTypePrefabDictionary _towerPrefabs;
        [SerializeField]
        private Text _purchasableTowerText;
        [SerializeField]
        private GameObject _pausePanel;
        [SerializeField]
        private Text[] _expTexts;
        [SerializeField]
        private Text[] _levelTexts;


        private PoolManager _pools;
        private PlayerControls _controls;
        private Upgrades _upgrades;
        private Tower _dragTower;
        private Tower _selectedTower;
        

        private int[] _maxUpgrades;
        private int[] _currentUpgrades;
        
        private float _camRayLength = 10000f;
        private int _layerMaskInstantiatedTowers;
        private int _layerMaskCamRaySurface;
        private BuildColliderComponent _currentTowerBuildComponent;

        private Animation _leftPanelAnimation;
        private Animation _rightPanelAnimation;
        private AnimationState _leftOpen;
        private AnimationState _rightOpen;
        private bool _upgradePanelOpen;
        private bool _isPause;

        private Dictionary<string, UpgradeData> _upgradeDatas = new Dictionary<string, UpgradeData>();
        private Dictionary<TowerType, TowerData> _towerDatas = new Dictionary<TowerType, TowerData>();
        private TowerType _currentTowerBuildType = TowerType.DartTower;
        private UpgradeButtonComponent[][] _buttons;
        private Text[][] _buttonsText;
        private List<ActionBase> _towers = new List<ActionBase>();

        [NonSerialized]
        public bool PointerOnUI;
        [NonSerialized]
        public bool IsDragTower;
        [NonSerialized]
        public bool ReadyToBuild;

        private void Awake()
        {
            Helper.PanelController = this;
            _pools = GetComponent<PoolManager>();
            _buttons = new UpgradeButtonComponent[2][] { _leftButtons, _rightButtons };
            _buttonsText = new Text[2][] { _leftButtonsText, _rightButtonsText };
            _layerMaskInstantiatedTowers = LayerMask.GetMask("Tower");
            _layerMaskCamRaySurface = LayerMask.GetMask("CamRaySurface");
            foreach (var data in Resources.Load<TowerDatas>("TowerDatas").BaseTowerDatas)
            {
                _towerDatas.Add(data.TowerType, data);
            }
            _controls = new PlayerControls();
            _controls.Enable();
            _controls.MouseInteractions.Click.performed += OnMouseClick;
            _controls.KeyboardControls.Escape.performed += OnEscape;
            _upgrades = new Upgrades();
            _leftPanelAnimation = _upgradePanelLeft.GetComponent<Animation>();
            _rightPanelAnimation = _upgradePanelRight.GetComponent<Animation>();
            _leftOpen = _leftPanelAnimation["LeftPanelAnimation"];
            _rightOpen = _rightPanelAnimation["RightPanelAnimation"];


            LoadUpgradeDatas();
        }

        private void OnDisable()
        {
            _controls.MouseInteractions.Click.performed -= OnMouseClick;
            _controls.KeyboardControls.Escape.performed -= OnEscape;
            _controls.Dispose();
        }

        private void OnEscape(CallbackContext context)
        {
            if (!_isPause)
            {
                UndoInstantiateTower();
                _isPause = true;
                Time.timeScale = 0f;
                _pausePanel.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }
        public void ResumeGame()
        {
            _isPause = false;
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
        }

        private void Update()
        {
            if (PointerOnUI) return;
            if (!IsDragTower) return;
            Ray camRay = Camera.main.ScreenPointToRay(_controls.MouseInteractions.MousePosition.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, _camRayLength, _layerMaskCamRaySurface))
            {
                _dragTower.transform.position = hit.point;
            }

        }

        public void SelectTower(TowerType type)
        {
            _purchasableTowerText.text = _towerDatas[type].Name;
            if (_gameManager.Money < _towerDatas[type].Cost * Helper.GetCostModifier()) return;
            _currentTowerBuildType = type;
            ReadyToBuild = true;
        }

        public TowerData GetTowerData(TowerType type)
        {
            return _towerDatas[type];
        }

        public void PreInstantiateTower()
        {
            if(!(_selectedTower is null)) _selectedTower.MainActionRange.gameObject.SetActive(false);
            CloseUpgradePanel();
            _dragTower = Instantiate(_towerPrefabs[_currentTowerBuildType]);
            _dragTower.transform.position = new Vector3(0, 1000, 1000);
            var range = _dragTower.ThisActionComponents.First(t => t.Main).ActionRange/5;
            _dragTower.MainActionRange.localScale = new Vector3(range, 0, range);
            _dragTower.MainActionRange.gameObject.SetActive(true);
            _currentTowerBuildComponent = _dragTower.GetComponent<BuildColliderComponent>();
            ReadyToBuild = false;
            IsDragTower = true;            
        }

        public void UndoInstantiateTower()
        {
            if (!IsDragTower) return;
            Destroy(_dragTower.gameObject);
            _dragTower = null;
            IsDragTower = false;
        }


        private void LoadUpgradeDatas()
        {
            foreach (var upgrades in Resources.LoadAll<UpgradesScripableObject>("UpgradeDatas"))
            {
                foreach (var data in upgrades.FisrtRow)
                {
                    var rewriteID = data.ID;
                    rewriteID = upgrades.TowerType.ToString().ToUpper() + "_" + rewriteID;
                    _upgradeDatas.Add(rewriteID, data);
                }
                foreach (var data in upgrades.SecondRow)
                {
                    var rewriteID = data.ID;
                    rewriteID = upgrades.TowerType.ToString().ToUpper() + "_" + rewriteID;
                    _upgradeDatas.Add(rewriteID, data);
                }
                foreach (var data in upgrades.ThirdRow)
                {
                    var rewriteID = data.ID;
                    rewriteID = upgrades.TowerType.ToString().ToUpper() + "_" + rewriteID;
                    _upgradeDatas.Add(rewriteID, data);
                }
            }
        }

       

        public void BuyUpgrade(UpgradeRowType upgradeRowType)
        {
            var id = ConvertValuesToId(_selectedTower.TowerType, upgradeRowType, _currentUpgrades[(int)upgradeRowType] + 1);
            float cost = _upgradeDatas[id].Cost * Helper.GetCostModifier();

            if (cost > _gameManager.Money) return;
            _gameManager.SubtractMoney(cost);
            _upgrades.PerformUpgrade(_selectedTower, id);
            CalculateUpgradeValues(upgradeRowType);
        }


        private void CalculateUpgradeValues(UpgradeRowType upgradeRowType)
        {
            int row = (int)upgradeRowType;
            _currentUpgrades[row]++;
            if (_currentUpgrades[row] > 2)
            {
                for (int i = 0; i < _maxUpgrades.Length; i++)
                {
                    if (i == row) continue;
                    if (_maxUpgrades[i] > 2) _maxUpgrades[i] = 2;
                }
            }
            if (_currentUpgrades.Where(t => t > 0).Count() > 1)
            {
                _maxUpgrades[Array.IndexOf(_currentUpgrades, 0)] = 0;
            }
            SetPanelParameters();
        }


        private void InstantiateTower()
        {
            foreach(var action in _towers.Where(t => t is ActionBuff))
            {
                if (!action.enabled) continue;
                action.GetComponentInChildren<BuffVisualAffection>().SetBaseColors();
            }
            _gameManager.SubtractMoney(_towerDatas[_currentTowerBuildType].Cost * Helper.GetCostModifier());            
            _dragTower.enabled = true;
            _dragTower.MainActionRange.gameObject.SetActive(false);
            _pools.GetEnemiesAndWaypoints(out var enemies, out var waypoints);
            _dragTower.ConfigureTower(enemies, _towers, waypoints);
            Destroy(_currentTowerBuildComponent);
            _currentTowerBuildComponent = null;
            foreach (var action in _dragTower.GetComponents<ActionBase>())
            {
                _towers.Add(action);
                if (action.Main) action.enabled = true;
            }
            IsDragTower = false;
            _dragTower = null;
        }



        private void OnMouseClick(CallbackContext context)
        {
            if (PointerOnUI) return;
            if (IsDragTower)
            {
                if (_currentTowerBuildComponent.DisableToBuild) return;
                InstantiateTower();
                return;
            }


            Ray camRay = Camera.main.ScreenPointToRay(_controls.MouseInteractions.MousePosition.ReadValue<Vector2>());
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, _camRayLength, _layerMaskInstantiatedTowers))
            {
                if (!(_selectedTower is null)) _selectedTower.MainActionRange.gameObject.SetActive(false);
                _selectedTower = hit.collider.GetComponentInParent<Tower>();
                var range = _selectedTower.ThisActionComponents.First(t => t.Main).ActionRange/5;
                _selectedTower.MainActionRange.localScale = new Vector3(range, 0, range);
                _selectedTower.MainActionRange.gameObject.SetActive(true);
                _maxUpgrades = _selectedTower.GetMaxUpgrades();
                _currentUpgrades = _selectedTower.GetCurrentUpgrades();
                SetPanelParameters();
                OpenUpgradePanel();

            }
            else
            {
                if (_selectedTower is null) return;
                _selectedTower.MainActionRange.gameObject.SetActive(false);
                CloseUpgradePanel();
            }



        }


        private void OpenUpgradePanel()
        {
            if (_upgradePanelLeft.Open || _upgradePanelRight.Open) return;
            if (_selectedTower.transform.position.x > 100)
            {
                if (!_upgradePanelLeft.Open)
                {
                    _leftOpen.time = 0;
                    _leftOpen.speed = 2;
                    _leftPanelAnimation.Play();
                    _upgradePanelLeft.Open = true;

                }


            }
            else
            {
                if (!_upgradePanelRight.Open)
                {
                    _rightOpen.time = 0;
                    _rightOpen.speed = 2;
                    _rightPanelAnimation.Play();
                    _upgradePanelRight.Open = true;

                }
            }
        }

        private void CloseUpgradePanel()
        {
            if (_upgradePanelLeft.Open)
            {

                _leftOpen.time = _leftOpen.length;
                _leftOpen.speed = -2;
                _leftPanelAnimation.Play();// ("LeftPanelAnimation");


                _upgradePanelLeft.Open = false;
                //_leftPanelAnimation.Play("LeftPanelClose");
            }
            if (_upgradePanelRight.Open)
            {

                _rightOpen.time = _leftOpen.length;
                _rightOpen.speed = -2;
                _rightPanelAnimation.Play();// ("LeftPanelAnimation");


                _upgradePanelRight.Open = false;
                //_leftPanelAnimation.Play("LeftPanelClose");
            }
        }

        public void UpdatePanel(TowerType towerType)
        {
            if (_selectedTower is null) return;
            if (_selectedTower.TowerType == towerType) SetPanelParameters();
        }

        private void SetPanelParameters()
        {
            if (_selectedTower == null) return;

            for(int p = 0; p < 2; p++)
            {
                if(Helper.ExpManager.GetExpToLevelup(_selectedTower.TowerType, out var exp))
                {
                    _expTexts[p].text = Helper.ExpManager.GetExp(_selectedTower.TowerType).ToString() + " / " + exp;
                }
                else
                {
                    _expTexts[p].text = "Max Level!";
                }

                _levelTexts[p].text = Helper.ExpManager.GetLevel(_selectedTower.TowerType).ToString();

                for (int i = 0; i < 3; i++)
                {
                    
                    

                    for (int current = 0; current < 5; current++)
                    {
                        if (current < _currentUpgrades[i]) _buttons[p][i].ToggleUpgrades[current].isOn = true;
                        else _buttons[p][i].ToggleUpgrades[current].isOn = false;

                    }

                    if (_currentUpgrades[i] >= _maxUpgrades[i] || _currentUpgrades[i] >= Helper.ExpManager.GetLevel(_selectedTower.TowerType))
                    {
                        _buttons[p][i].ThisButton.interactable = false;
                        _buttons[p][i].Text.text = UnableToUpgradeText;
                    }
                    else
                    {
                        _buttons[p][i].ThisButton.interactable = true;
                        //print(ConvertValuesToId(_selectedTower.TowerType, _buttons[p][i].UpgradeRowType, _currentUpgrades[i] + 1));
                        string newUpgradeId = ConvertValuesToId(_selectedTower.TowerType, _buttons[p][i].UpgradeRowType, _currentUpgrades[i] + 1);
                        string oldUpgradeId = ConvertValuesToId(_selectedTower.TowerType, _buttons[p][i].UpgradeRowType, _currentUpgrades[i]);

                        _buttons[p][i].Text.text = _upgradeDatas[newUpgradeId].Name;
                        if (_upgradeDatas.ContainsKey(oldUpgradeId)) _buttonsText[p][i].text = _upgradeDatas[oldUpgradeId].Name;
                        else _buttonsText[p][i].text = "No upgrades";

                    }
                }
                    
            }
        }

        public string ConvertValuesToId(TowerType towerType, UpgradeRowType upgradeRowType, int number)
        {
            string id = string.Format("{0}_ROW_{1}_UPGRADE_{2}", towerType.ToString().ToUpper(), upgradeRowType.ToString().ToUpper(), number.ToString());
            return id;
        }

      







       

        

    }

}