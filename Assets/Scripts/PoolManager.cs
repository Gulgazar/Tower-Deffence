using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TowerDeffence
{
    public class PoolManager : MonoBehaviour, IProjectileHandler, IEnemyHandler
    {


        private Dictionary<EnemyType, EnemyData> _enemiesDatas = new Dictionary<EnemyType, EnemyData>();
        private Dictionary<ProjectileType, List<BaseProjectileComponent>> _projectilesPool = new Dictionary<ProjectileType, List<BaseProjectileComponent>>();
        private Dictionary<EnemyType, List<BaseEnemyComponent>> _enemiesPool = new Dictionary<EnemyType, List<BaseEnemyComponent>>();

        private List<Transform> _mainWayPoints = new List<Transform>();
        private List<float> _distancesToNextWayPoint = new List<float>();
        private List<Vector3> _directionsToNextWayPoint = new List<Vector3>();
        private List<BaseEnemyComponent> _activeEnemies = new List<BaseEnemyComponent>();

        [SerializeField]
        private EnemyTypeControllerDictionary _enemyPrefabs;
        [SerializeField]
        private ProjectileTypeControllerDictionary _projectilePrefabs;
        [SerializeField]
        private Transform _enemiesPoolGameObject;
        [SerializeField]
        private Transform _projectilesPoolGameObject;
        private GameManager _gameManager;
        private string _enemyTag;

        #region Base

        private void Awake()
        {
            _enemyTag = Helper.EnemyTag;
            Helper.ProjectileHandler = this;
            Helper.EnemyHandler = this;

            foreach(var EnemyData in Resources.Load<EnemySettings>("Enemies").Enemies)
            {
                _enemiesDatas.Add(EnemyData.EnemyType, EnemyData);
            }
            foreach(EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                _enemiesPool.Add(type, new List<BaseEnemyComponent>());
            }
            foreach (ProjectileType type in Enum.GetValues(typeof(ProjectileType)))
            {
                _projectilesPool.Add(type, new List<BaseProjectileComponent>());
            }            

        }

        public bool GetLeftEnemiesCount()
        {
            foreach(var pair in _enemiesPool)
            {
                foreach(var enemy in pair.Value)
                {
                    if (enemy.gameObject.activeSelf)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void GetEnemiesAndWaypoints(out List<BaseEnemyComponent> enemiesPool, out List<Transform> waypoints)
        {
            enemiesPool = _activeEnemies;
            waypoints = _mainWayPoints;
        }

        public void SetWayPoints(GameManager gm, List<Transform> main, List<float> distances, List<Vector3> directions)
        {
            _gameManager = gm;
            _mainWayPoints = main;
            _distancesToNextWayPoint = distances;
            _directionsToNextWayPoint = directions;
        }

        private void FixedUpdate()
        {
            OperateProjectiles();
            MoveAllEnemies();
        }

        #endregion

        #region Projectiles
        private void OperateProjectiles()
        {
            foreach (var pair in _projectilesPool)
            {
                foreach (var projectile in pair.Value)
                {
                    if (!projectile.enabled) continue;
                    /*
                    if (projectile.TargetsLeft == 0)
                    {
                        DestroyProjectile(projectile);
                        continue;
                    }
                    */
                    if (projectile.LifeTime <= 0)
                    {
                        DestroyProjectile(projectile);
                        continue;
                    }
                    projectile.transform.Translate(Vector3.forward * projectile.SourceTower.ProjectileSpeed * Time.fixedDeltaTime);
                    projectile.LifeTime -= Time.fixedDeltaTime;
                }
            }
        }

       
        


        

        private BaseProjectileComponent GetNewProjectile(ProjectileType type)
        {
            BaseProjectileComponent newProjectile;
            foreach(var projectile in _projectilesPool[type])
            {
                if (projectile.gameObject.activeSelf) continue;
                newProjectile = projectile;
                newProjectile.gameObject.SetActive(true);
                newProjectile.ClearEnemiesList();
                return newProjectile;
            }
            newProjectile = Instantiate(_projectilePrefabs[type]);
            newProjectile.transform.parent = _projectilesPoolGameObject;
            //var data = 
            _projectilesPool[type].Add(newProjectile);
            return newProjectile;
        }

        public void CreateProjectile(ActionAttack source)
        {

            var projectile = GetNewProjectile(source.ProjectileType);
            projectile.ConfigureProjectile(source);
            projectile.transform.position = source.AttackPoint.position;
            projectile.transform.rotation = source.AttackPoint.rotation;
            
        }

       
        
        public void DestroyProjectile(BaseProjectileComponent projectile)
        {
            projectile.gameObject.SetActive(false);
        }



        #endregion

        #region Enemies

        private void SpawnEnemyChildren(BaseEnemyComponent mainParent, int excessiveDamage, BaseProjectileComponent source = null)
        {
            List<EnemyType> childrenForSpawnList = new List<EnemyType>();
            List<BaseEnemyComponent> children = new List<BaseEnemyComponent>();
            foreach(EnemyType child in _enemiesDatas[mainParent.Type].Children)
            {
                GetEnemyChildrenForSpawn(childrenForSpawnList, child, excessiveDamage);
            }
            foreach(EnemyType child in childrenForSpawnList)
            {
                var spawnedChild = SpawnEnemy(child, mainParent.transform.position, mainParent.NextPoint, mainParent.DistanceToNextPoint, mainParent.Camo);
                children.Add(spawnedChild);
            }
            if(!(source is null)) source.AddAffectedChildren(children);

            

        }
        private void GetEnemyChildrenForSpawn(List<EnemyType> ChildrenForSpawnList, EnemyType recursiveParent, int excessiveDamage)
        {
            excessiveDamage -= _enemiesDatas[recursiveParent].Health;
            //Если шар не может быть убит потенциальным уроном, то появляется с полным ХП.
            if(excessiveDamage < 0)
            {
                ChildrenForSpawnList.Add(recursiveParent);
                return;
            }
            _gameManager.AddMoney(1);
            foreach(EnemyType child in _enemiesDatas[recursiveParent].Children)
            {
                GetEnemyChildrenForSpawn(ChildrenForSpawnList, child, excessiveDamage);
            }
        }


   

        public void KillEnemy(BaseEnemyComponent enemy, int excessiveDamage = 0, BaseProjectileComponent source = null)
        {
            //print(excessiveDamage);
            _gameManager.AddMoney(1);
            SpawnEnemyChildren(enemy, excessiveDamage, source);
            DestroyEnemy(enemy);
        }

        public void DestroyEnemy(BaseEnemyComponent enemy)
        {
            _activeEnemies.Remove(enemy);
            enemy.gameObject.SetActive(false);
        }

        public BaseEnemyComponent SpawnEnemy(EnemyType enemyType, Vector3 position, int nextPoint, float distanceToNextPoint, bool camo)
        {
            var enemy = GetNewEnemy(enemyType, camo);
            _activeEnemies.Add(enemy);
            enemy.Health = _enemiesDatas[enemyType].Health;
            enemy.transform.position = position;
            enemy.NextPoint = nextPoint;
            enemy.DistanceToNextPoint = distanceToNextPoint;
            if(enemy.IsBoss) enemy.MeshGameObject.LookAt(_mainWayPoints[enemy.NextPoint]);
            return enemy;
        }

        private void MoveAllEnemies()
        {
            foreach(var pair in _enemiesPool)
            {
                foreach(var enemy in pair.Value)
                {
                    if (!enemy.gameObject.activeSelf) continue;
                    MoveEnemy(enemy, out var isReachedFinish);
                    
                    if (isReachedFinish)
                    {
                        int damage = 0;
                        damage ++;
                        GetFullChildrenHealth(enemy.Type, ref damage);
                        _gameManager.SubtractHealth(damage);
                        DestroyEnemy(enemy);
                        
                    }
                    
                }
                
            }
        }

       

        private void GetFullChildrenHealth(EnemyType parent, ref int damage)
        {            
            foreach(EnemyType child in _enemiesDatas[parent].Children)
            {
                damage ++;
                GetFullChildrenHealth(child, ref damage);
            }
        }



        private BaseEnemyComponent GetNewEnemy(EnemyType type, bool camo)
        {
            BaseEnemyComponent newEnemy;
            foreach (var enemy in _enemiesPool[type])
            {
                if (enemy.gameObject.activeSelf) continue;

                newEnemy = enemy;
                newEnemy.gameObject.SetActive(true);
                return newEnemy;
            }
            newEnemy = Instantiate(_enemyPrefabs[type]);
            newEnemy.transform.parent = _enemiesPoolGameObject;
            newEnemy.tag = _enemyTag;
            var data = _enemiesDatas[type];
            newEnemy.Type = type;
            newEnemy.Children = data.Children;
            newEnemy.Lead = data.Lead;
            newEnemy.Camo = camo;
            newEnemy.IsBoss = data.IsBoss;
            newEnemy.SpeedModifier = data.SpeedModifier;
            _enemiesPool[type].Add(newEnemy);
            return newEnemy;
        }

        private void MoveEnemy(BaseEnemyComponent enemy, out bool isReachedFinish)
        {
            var step = enemy.SpeedModifier * Helper.BaseSpeed * Time.fixedDeltaTime;
            if (enemy.DistanceToNextPoint > step)
            {
                enemy.DistanceToNextPoint -= step;
                enemy.transform.Translate(_directionsToNextWayPoint[enemy.NextPoint] * step);
                isReachedFinish = false;
                return;
            }
            if (enemy.NextPoint + 1 == _mainWayPoints.Count)
            {
                //DestroyEnemy(enemy);

                isReachedFinish = true;
                return;
            }

            var excessive = step - enemy.DistanceToNextPoint;
            enemy.transform.position = _mainWayPoints[enemy.NextPoint].localPosition;
            enemy.NextPoint++;
            enemy.DistanceToNextPoint = _distancesToNextWayPoint[enemy.NextPoint] - excessive;
            enemy.transform.Translate(_directionsToNextWayPoint[enemy.NextPoint] * excessive);
            if (enemy.IsBoss)
            {
                enemy.MeshGameObject.LookAt(_mainWayPoints[enemy.NextPoint]);
                /*
                var rotation = enemy.MeshGameObject.rotation;
                rotation.y = 0;
                enemy.MeshGameObject.rotation = rotation;
                */
            }

            isReachedFinish = false;

        }
        #endregion

    }
}