using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Wata.Extension;
using Wata.Extension.Test;
using Random = UnityEngine.Random;

namespace Wata.MapGenerator {
    
    public class MapGenrator: MonoBehaviourWrapper {

        #region Test

        [SerializeField] private StageTypeFrequencyDataTable _testData;

        private void TestSetUp() {
            StageTypeFrequency.LoadData(_testData);
        }

        [TestFunction(runtimeOnly: true)]
        private void Generate() {
            
            GenerateMap();
        }
        
        #endregion
        
        //==================================================||SerializeFields 
        [Header("Position")]
        [SerializeField] private Vector2 _randomNoise = new(0.8f, 0.8f);
        
        [Space]
        [Header("Round Info")]
        [SerializeField] private int _roundCount;
        [SerializeField] private StageWidthFrequencyDataTable _roundWidthFrequency;
        [SerializeField] private MapNode _roundSymbol;

        [Space] 
        [Header("Prefabs")] 
        [SerializeField] private Image _edge;
        
        //==================================================||Fields 
        private List<List<MapNode>> _mapNodes = new();
        private List<Image> _edges = new();
        
        //==================================================||Methods 

        private void GenerateMap() {
            
            _edges.ForEach(edge => Destroy(edge.gameObject));
            _edges.Clear();            
            
            foreach (var floor in _mapNodes) {
                floor.ForEach(node => Destroy(node.gameObject));
                floor.Clear();
            }
            _mapNodes.Clear();

            var interval = 1f / (_roundCount + 1);
            for (int i = 0; i < _roundCount; i++) {
                GenerateRound(interval * (i + 1));
                GenerateEdges();
            }
        }
       
        private Vector2 RandomNoise(Vector2 pSize) {
            
            var maxSize = pSize * _randomNoise;
            var x = Random.Range(-maxSize.x, maxSize.x);
            var y = Random.Range(-maxSize.y, maxSize.y);
            return new(x, y);
        }
        
        private void GenerateRound(float pHeight) {

            _mapNodes.Add(new());
            
            var width = GetWidthSize() + 1;
            var interval = 1f / width;
            
            for (int i = 1; i < width; i++ ) {
                var newIcon = Instantiate(_roundSymbol, transform);

                var rect = newIcon.transform as RectTransform;
                
                //set position
                rect.SetLocalPositionX(PivotLocation.Down, i * interval); 
                rect.SetLocalPositionY(position: pHeight);
                rect.AddPosition(RandomNoise(rect.sizeDelta));
                
                //set type
                var type = StageTypeFrequency.Random();
                newIcon.SetIcon(type);
                
                _mapNodes[^1].Add(newIcon);
            }

            _mapNodes[^1] = _mapNodes[^1]
                .OrderBy(node => node.transform.position.x)
                .ToList();
        } 
        
        private int GetWidthSize() {
            var count = _roundWidthFrequency.Table.Sum(floor => floor.Frequency);
            var random = Random.Range(0, count);

            var idx = 0;
            foreach (var floor in _roundWidthFrequency.Table) {
                idx += floor.Frequency;
                if (idx >= random)
                    return floor.Width;
            }

            throw new ArgumentOutOfRangeException();
        }

        private void GenerateEdges() {
            if (_mapNodes.Count <= 1)
                return;

            var isAfter = _mapNodes[^1].Count > _mapNodes[^2].Count;
            var idx1 = 0;
            foreach (var node in isAfter ? _mapNodes[^1] : _mapNodes[^2]) {
                int target = 0;
                var distance = 1000f;
                var idx2 = 0;
                foreach (var otherNode in isAfter ? _mapNodes[^2] : _mapNodes[^1]) {

                    var curDistance = (node.transform.position - otherNode.transform.position).magnitude;
                    if (distance >= curDistance) {
                        target = idx2;
                        distance = curDistance;
                    }

                    idx2++;
                }

                var edge = Instantiate(_edge, transform);
                var size = edge.rectTransform.sizeDelta;
                size.y = distance;
                edge.rectTransform.sizeDelta = size;

                var delta = Vector2.zero;
                if (isAfter) {
                    edge.transform.position = _mapNodes[^2][target].transform.position;
                    delta = node.transform.position - edge.transform.position;
                    _mapNodes[^2][target].Add(idx1);
                }
                else {
                    
                    edge.transform.position = node.transform.position;
                    delta = _mapNodes[^1][target].transform.position - edge.transform.position;
                    node.Add(target);
                }
                var direction = Mathf.Atan2(delta.y, delta.x);
                edge.transform.rotation = Quaternion.Euler(0, 0, direction * Mathf.Rad2Deg - 90);
                _edges.Add(edge);

                idx1++;
            }
            
        }
        
        //==================================================||Unity

        private void Awake() {
            TestSetUp();
        
            GenerateMap();
        }
    }
}