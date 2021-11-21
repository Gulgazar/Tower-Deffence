using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDeffence
{
    public class BuffVisualAffection : MonoBehaviour
    {
        
        [SerializeField]
        private Color _colorAffected;
        [SerializeField]
        private Color _colorBase;

        private List<MeshRenderer> _meshRenderers;


        public List<MeshRenderer> ThisMeshRenderers;
        private void Awake()
        {
            ThisMeshRenderers = transform.parent.GetComponentsInChildren<MeshRenderer>().Where(t => t.tag != "AttackRange").ToList();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Tower") return;
            if(other.transform.parent.TryGetComponent<BuildColliderComponent>(out var _))
            {
                foreach(var mesh in ThisMeshRenderers)
                {
                    mesh.materials[1].color = _colorAffected;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent<BuildColliderComponent>(out var _))
            {
                SetBaseColors();
            }

        }

        public void SetBaseColors()
        {
            foreach (var mesh in ThisMeshRenderers)
            {
                mesh.materials[1].color = _colorBase;
            }
        }


    }

}