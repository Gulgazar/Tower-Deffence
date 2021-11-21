using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Maps/Config")]
    public class MapWayPoints : ScriptableObject
    {
        public MapType MapType;
        public Material MapPicture;
        public AudioClip Soundtrack; 
        public int Money;
        

        //public Transform[] Waypoints;
        [Serializable]
        public class BlockedTerritory
        {
            public Vector3 Position;
            public Quaternion Rotation;
            //public Collider ColliderThis;
            public ColliderType ColliderType;
            public Vector3 CubeSize;
            public float SphereRadius;
        }

        public enum ColliderType
        {
            Sphere,
            Box,
            Capsule
        }

        //[NonSerialized]
        public List<Vector3> Vectors;
        public List<BlockedTerritory> BlockColliders;

        [ContextMenu("ConfigureBlockedTerritory")]
        private void GetBlockedTerritory()
        {
            BlockColliders = new List<BlockedTerritory>();
            var blockedPool = GameObject.FindGameObjectWithTag("BlockedTerritoryPool").transform;
            Debug.Log(blockedPool.GetComponentsInChildren<Collider>().ToList().Count);
            foreach(var block in blockedPool.GetComponentsInChildren<Collider>())
            {
                var blockCfg = new BlockedTerritory()
                {
                    Position = block.transform.position,
                    Rotation = block.transform.rotation
                                    
                };
                if (block is BoxCollider)
                {
                    blockCfg.ColliderType = ColliderType.Box;
                    blockCfg.CubeSize = ((BoxCollider)block).size;
                }
                else if (block is SphereCollider)
                {
                    blockCfg.ColliderType = ColliderType.Sphere;
                    blockCfg.SphereRadius = ((SphereCollider)block).radius;
                }
                BlockColliders.Add(blockCfg);
            }
        }


        [ContextMenu("ConfigreWaypoints")]
        private void GetVectors()
        {
            Vectors = new List<Vector3>();
            var pool = GameObject.FindGameObjectWithTag("EditorWaypointsPool").transform;
            foreach (var wp in pool.GetComponentsInChildren<Transform>())
            {
                if (wp == pool) continue;
                Vectors.Add(wp.position);
            }

        }
    }

}