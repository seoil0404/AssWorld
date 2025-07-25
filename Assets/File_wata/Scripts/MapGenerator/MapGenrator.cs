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
            StageTypeFrequency.LoadFrequency(_testData);
        }

        [TestFunction(runtimeOnly: true)]
        private void GenerateTest() {
            _mapNodes.ForEach(Destroy);
            _mapNodes.Clear();
            GenerateRound(0.1f);
        }

        #endregion
        
       //==================================================||SerializeFields 
        [Header("Positio")]
        [SerializeField] private Vector2 _randomNoise = new(0.8f, 0.8f);
        
        [Space]
        [Header("Round Info")]
        [SerializeField] private int _roundCount;
        [SerializeField] private StageWidthFrequencyDataTable _roundWidthFrequency;
        [SerializeField] private Image _roundSymbol;

       //==================================================||Fields 
        private Dictionary<Stage, Sprite> _mapIcons = null;
        private List<GameObject> _mapNodes = new();

       //==================================================||Methods 
        private void FindIcons() {

            _mapIcons ??= Resources
                .LoadAll<Sprite>("StageIcons")
                .ToDictionary(
                    sprite => {
                        var name = Regex.Match(sprite.name, @"(.*)_\d").Groups[1].Value;
                        var stage = Enum.Parse(typeof(Stage), name);
                        return (Stage)stage;
                    },
                    sprite => sprite
                );
        }

        private Vector2 RandomNoise(Vector2 pSize) {
            
            var maxSize = pSize * _randomNoise;
            var x = Random.Range(-maxSize.x, maxSize.x);
            var y = Random.Range(-maxSize.y, maxSize.y);
            return new(x, y);
        }
        
        private void GenerateRound(float pHeight) {

            var width = GetWidthSize() + 1;
            var interval = 1f / width;
            
            for (int i = 1; i < width; i++ ) {
                var newIcon = Instantiate(_roundSymbol, transform);
                
                //set position
                newIcon.rectTransform.SetLocalPositionX(PivotLocation.Down, i * interval); 
                newIcon.rectTransform.SetLocalPositionY(position: pHeight);
                newIcon.rectTransform.AddPosition(RandomNoise(newIcon.rectTransform.sizeDelta));
                
                //set image and type
                var type = StageTypeFrequency.Random();
                newIcon.sprite = _mapIcons[type];
                
                _mapNodes.Add(newIcon.gameObject);
            }
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
        
       //==================================================||Unity

       private void Awake() {
           TestSetUp();
           
           FindIcons();
           GenerateRound(0.1f);
       }
    }
}