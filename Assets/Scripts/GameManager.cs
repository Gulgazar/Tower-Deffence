using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

namespace TowerDeffence
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private BaseEnemyComponent _baseEnemyPrefab;
        [SerializeField]
        private Transform _wayPointPrefab;
        [SerializeField]
        private Transform _wayPointsPool;
        [SerializeField]
        private Transform _obstaclesPool;
        [SerializeField]
        private List<Vector3> _editorWayPoints;// = new List<Vector3>();
        [SerializeField]
        private Text _healthText;
        [SerializeField]
        private Text _moneyText;
        [SerializeField]
        private GameObject _gameResultPanel;
        [SerializeField]
        private Text _gameResultText;
        [SerializeField]
        private MeshRenderer _level;
        [SerializeField]
        private AudioSource _audioSource;

        private PoolManager _pools;
        private float _wayPointScale;
        private List<Transform> _mainWayPoints = new List<Transform>();
        private List<float> _distancesToNextWayPoint = new List<float>();
        private List<Vector3> _directionsToNextWayPoint = new List<Vector3>();
        private int _roundNumber = 0;
        private Transform _spawnPoint;
        private int _currentWave;
        private bool _progressSaved;
        private MapWayPoints _mapConfig;
        //private static List<BaseEnemyComponent> _enemies = new List<BaseEnemyComponent>();

        //private List<BaseProjectileComponent> _projectiles = new List<BaseProjectileComponent>();

        private List<Wave> _waves;

        public int BaseWaveMoney = 100;
        public int AdditionalWaveMoney = 5;

        public float Money;
        public int Health;


        private void Awake()
        {
            _mapConfig = Resources.LoadAll<MapWayPoints>("Maps").ToList().First(t => t.MapType == Helper.MapType);
            _editorWayPoints = _mapConfig.Vectors;
            _level.material = _mapConfig.MapPicture;
        }

        private void Start()
        {

            Money = _mapConfig.Money;
            UpdateHealth();
            UpdateMoney();



            _waves = Resources.Load<WavesSettings>("Scenarios/MAIN").Waves;
            _pools = GetComponent<PoolManager>();
            _wayPointScale = _wayPointPrefab.localScale.x * 4; //MagicValue

            for (int i = 0; i < _editorWayPoints.Count - 1; i++)
            {
                //_editorWayPoints[i].gameObject.SetActive(false);
                var editorRelativePosition = _editorWayPoints[i + 1] - _editorWayPoints[i];
                var distance = editorRelativePosition.magnitude - _wayPointScale;
                if (distance <= 0) print("ALLERT");
                var wayPointsCount = Mathf.RoundToInt(distance / _wayPointScale);
                var distanceBetweenWayPoints = distance / wayPointsCount;
                for (float k = 0; k <= wayPointsCount; k++)
                {
                    var wayPoint = Instantiate(_wayPointPrefab);
                    wayPoint.position = _editorWayPoints[i] + editorRelativePosition.normalized * distanceBetweenWayPoints * (k + 1);
                    wayPoint.Rotate(0, Random.Range(0, 3) * 90, 0);
                    //wayPoint.position = new Vector3(wayPoint.position.x, 0, wayPoint.position.z);
                    wayPoint.parent = _wayPointsPool;
                    _mainWayPoints.Add(wayPoint);
                }
            }
            //_editorWayPoints[_editorWayPoints.Count - 1].gameObject.SetActive(false);
            _spawnPoint = Instantiate(_wayPointPrefab);
            _spawnPoint.position = _editorWayPoints[0];
            _spawnPoint.Rotate(0, Random.Range(0, 3) * 90, 0);
            _spawnPoint.position = new Vector3(_spawnPoint.position.x, 0, _spawnPoint.position.z);
            var mainRelativePosition = _mainWayPoints[0].position - _spawnPoint.position;
            _distancesToNextWayPoint.Add(mainRelativePosition.magnitude);
            _directionsToNextWayPoint.Add(mainRelativePosition.normalized);

            for (int i = 0; i < _mainWayPoints.Count - 1; i++)
            {
                mainRelativePosition = _mainWayPoints[i + 1].position - _mainWayPoints[i].position;
                _distancesToNextWayPoint.Add(mainRelativePosition.magnitude);
                _directionsToNextWayPoint.Add(mainRelativePosition.normalized);
            }

            foreach(var blockCfg in _mapConfig.BlockColliders)
            {
                print(blockCfg);
                var block = new GameObject();
                

                block.transform.position = blockCfg.Position;
                block.transform.rotation = blockCfg.Rotation;
                if(blockCfg.ColliderType == MapWayPoints.ColliderType.Box)
                {
                    var collider = block.AddComponent<BoxCollider>();
                    collider.size = blockCfg.CubeSize;
                }
                else if (blockCfg.ColliderType == MapWayPoints.ColliderType.Sphere)
                {
                    var collider = block.AddComponent<SphereCollider>();
                    collider.radius = blockCfg.SphereRadius;
                }
                block.AddComponent<Rigidbody>().isKinematic = true;
                block.transform.parent = _obstaclesPool;
            }

            _pools.SetWayPoints(this, _mainWayPoints, _distancesToNextWayPoint, _directionsToNextWayPoint);
            _audioSource.clip = _mapConfig.Soundtrack;
            _audioSource.volume = PlayerPrefs.GetFloat("Music");
            _audioSource.Play();
            
            StartCoroutine(Waves());
        }


        public void AddMoney(int count)
        {
            Money += count;
            UpdateMoney();
        }

        public void SubtractMoney(float count)
        {
            Money -= count;
            UpdateMoney();
        }

        public void AddHealth(int count)
        {
            Health += count;
            UpdateHealth();
        }

        public void SubtractHealth(int count)
        {
            Health -= count;
            if (Health < 0)
            {
                Health = 0;
                UpdateHealth();
                Lose();
            }
            UpdateHealth();
        }

        private void UpdateMoney()
        {
            _moneyText.text = "$" + Mathf.RoundToInt(Money);
        }
        private void UpdateHealth()
        {
            _healthText.text = Health.ToString();
        }

        private IEnumerator Waves()
        {
            yield return new WaitForSeconds(3);
            foreach (var wave in _waves)
            {

                
                _currentWave = wave.WaveNumber;
                //print("WAVE  " + wave.WaveNumber);
                foreach (var stage in wave.Stages)
                {
                    //print("STAGE " + stage.EnemyType);
                    for (int i = 0; i < stage.EnemyCount; i++)
                    {
                        //print("ENEMY  " + i);
                        _pools.SpawnEnemy(stage.EnemyType, _spawnPoint.position, 0, _distancesToNextWayPoint[0], stage.Camo);
                        yield return new WaitForSeconds(stage.Cooldown);
                    }
                    yield return new WaitForSeconds(stage.CooldownToNextStage);
                }


                do
                {
                    yield return new WaitForSeconds(1);
                }
                while (_pools.GetLeftEnemiesCount());
                yield return new WaitForSeconds(3f);
                AddMoney(BaseWaveMoney + AdditionalWaveMoney * wave.WaveNumber);
                if (wave.WaveNumber == Helper.GetTotalWaves())
                {
                    Win();
                    yield break;
                }
            }
        }

        private void Win()
        {
            //SaveProgress();
            _progressSaved = true;
            Time.timeScale = 0f;
            _gameResultPanel.SetActive(true);
            _gameResultText.text = "Congratulations! You have finished this map on " + Helper.Difficulty.ToString().ToUpper() + " difficulty! Try out other difficulties and maps, play more to unlock other towers' upgrades!";
        }
        private void Lose()
        {
            //SaveProgress();
            _progressSaved = true;
            Time.timeScale = 0f;
            _gameResultPanel.SetActive(true);
            _gameResultText.text = "Oh no! You lost on wave " + _currentWave + ". Don't be upset, try again, or try out other difficulties and maps, play more to unlock other towers' upgrades!";
        }
        public void RestartLevel_UnityEditor()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        }

        public void ReturnToMenu_UnityEditor()
        {
            Time.timeScale = 1f;
            //if(!_progressSaved) SaveProgress();
            SceneManager.LoadScene("MainMenu");
        }
        

        private void SaveProgress()
        {
            print("SAVED!");
        }
       
       
    }
}