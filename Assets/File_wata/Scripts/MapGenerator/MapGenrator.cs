using System;
using System.Collections.Generic;
using System.Linq;
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
        
       //==================================================||Constant
       private const int topIntval = 2;
        
        //==================================================||SerializeFields 
        [Space]
        [Header("Target")]
        [SerializeField] private GameObject _map;
        
        [Space]
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
        
        //==================================================||Methods 

        private void GenerateMap() {
            
            foreach (var floor in _mapNodes) {
                floor.ForEach(node => Destroy(node.gameObject));
                floor.Clear();
            }
            _mapNodes.Clear();

            var interval = 1f / (_roundCount + 1 + topIntval);
            for (int i = 0; i < _roundCount; i++) {
                GenerateRound(interval * (i + 1), i);
                GenerateEdges();
            }
        }
       
        
        private void GenerateRound(float pHeight, int pIdx) {

            _mapNodes.Add(new());
            
            var width = GetWidthSize() + 1;
            var interval = 1f / width;
            
            for (int i = 1; i < width; i++ ) {
                var newIcon = Instantiate(_roundSymbol, _map.transform);

                var rect = newIcon.transform as RectTransform;
                
                //set position
                rect.SetLocalPositionX(PivotLocation.Down, i * interval); 
                rect.SetLocalPositionY(position: pHeight);
                rect.AddPosition(RandomNoise(rect.sizeDelta));
                
                //set type
                var type = StageTypeFrequency.Random();
                newIcon.Init(type, new(i, pIdx));
                
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
        private Vector2 RandomNoise(Vector2 pSize) {
                    
            var maxSize = pSize * _randomNoise;
            var x = Random.Range(-maxSize.x, maxSize.x);
            var y = Random.Range(-maxSize.y, maxSize.y);
            return new(x, y);
        }

        private void GenerateEdges() {
            if (_mapNodes.Count <= 1)
                return;

            var isAfter = _mapNodes[^1].Count > _mapNodes[^2].Count;
            var count = isAfter ? _mapNodes[^2].Count : _mapNodes[^1].Count;
            var visit = new int[count];
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

                if (isAfter) {
                    GenerateEdge(_mapNodes[^2][target], node, idx1);
                }
                else {
                    GenerateEdge(node, _mapNodes[^1][target], target);
                }

                visit[target] = idx1;
                
                idx1++;
            }
            
            for (int i = 0; i < count; i++) {
                if(visit[i] != 0)
                    continue;

                if (i == 0) {
                    GenerateEdge(_mapNodes[^2][0], _mapNodes[^1][0], 0);
                    continue;
                }

                if (i == count - 1) {
                    GenerateEdge(_mapNodes[^2][^1], _mapNodes[^1][^1], _mapNodes[^1].Count);
                    continue;
                }

                if (isAfter) {
                    var target = _mapNodes[^2][i - 1][^1].x;
                    GenerateEdge(_mapNodes[^2][i], _mapNodes[^1][target], target);
                }
                else {
                    var target = visit[i - 1];
                    GenerateEdge(_mapNodes[^2][target], _mapNodes[^1][i], i);
                }
            }
        }

        private void GenerateEdge(MapNode pStart, MapNode pEnd, int pEndIdx) {
            var edge = Instantiate(_edge, _map.transform);


            var delta = pEnd.transform.position - pStart.transform.position;
            edge.transform.position = pStart.transform.position;
            pStart.Add(pEndIdx, edge.gameObject);
                 
            //Edge setting
            var size = edge.rectTransform.sizeDelta;
            size.y = delta.magnitude;
            
            edge.rectTransform.sizeDelta = size;
            var direction = Mathf.Atan2(delta.y, delta.x);
            edge.transform.rotation = Quaternion.Euler(0, 0, direction * Mathf.Rad2Deg - 90);
            edge.rectTransform.SetSiblingIndex(0);
        }
        
        //==================================================||Unity

        private void Awake() {
            TestSetUp();
        
            GenerateMap();

            foreach (var node in _mapNodes[0]) {
                node.ActiveNode();
            }
        }
    }
}