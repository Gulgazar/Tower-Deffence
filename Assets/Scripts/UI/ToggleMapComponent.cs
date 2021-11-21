using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    public class ToggleMapComponent : MonoBehaviour
    {
        [SerializeField]
        private MapType MapType;
        
        public void OnToggle(bool isOn)
        {
            if(isOn) Helper.MapType = MapType;
        }


    }

}