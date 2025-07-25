using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Wata.MapGenerator {
    public class MapNode: MonoBehaviour, IEnumerable<int> {

       //==================================================||Properties
       public Stage Stage => _stageType;
        
      //==================================================||Serialize Field 
       [SerializeField] private Image _backGround;
       
       //==================================================||Fields 
        private Stage _stageType;
        private List<int> _nextNode = new();
        private static Dictionary<Stage, Sprite> mapIcons = null;
        
       //==================================================||Methods 
       
        public void Add(int pIdx) =>
            _nextNode.Add(pIdx);
        
        public void SetIcon(Stage pStage) {

            FindIcons();
            
            _stageType = pStage;
            _backGround.sprite = mapIcons[pStage];
        }
        
        private static void FindIcons() =>
            mapIcons ??= Resources
                .LoadAll<Sprite>("StageIcons")
                .ToDictionary(
                    sprite => {
                        var name = Regex.Match(sprite.name, @"(.*)_\d").Groups[1].Value;
                        var stage = Enum.Parse(typeof(Stage), name);
                        return (Stage)stage;
                    },
                    sprite => sprite
                );

        public IEnumerator<int> GetEnumerator() =>
            _nextNode.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            _nextNode.GetEnumerator();
    }
}