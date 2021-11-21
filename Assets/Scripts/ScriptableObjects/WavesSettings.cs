using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDeffence
{
    [CreateAssetMenu(fileName = "Scenario", menuName = "Scenarios")]
    public class WavesSettings : ScriptableObject
    {
        public string ScenarioID;
        public List<Wave> Waves;

    }



}