using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Wata.MapGenerator {
    
    public class MapGenrator: MonoBehaviour {

        [Header("Size")]
        [SerializeField] private float _width;
        [SerializeField] private float _heightInterval;
        
        [Space]
        [SerializeField] private Vector2 _randomNoise;
        
        [Space]
        [Header("Round Info")]
        [SerializeField] private int _roundCount;
        [SerializeField] private List<int> _roundWidthProbabbly;

    }
}