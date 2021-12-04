using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class BuildColliderComponent : MonoBehaviour
    {
        

        private Collider _collider;
        [SerializeField]
        private Collider _meshCollider;

        public bool DisableToBuild;
        public int _collisions;

        public List<Collider> _cds;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            var surface = GameObject.FindGameObjectWithTag("CamRaySurface");
            Physics.IgnoreCollision(_collider, surface.GetComponent<Collider>());
            Physics.IgnoreCollision(_collider, _meshCollider);
        }

        private void OnTriggerEnter(Collider other)
        {
            _collisions++;
            _cds.Add(other);
            DisableToBuild = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _collisions--;
            _cds.Remove(other);
            if(_collisions < 1) DisableToBuild = false;
        }
    }

}