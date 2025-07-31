using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Wata.Extension;

namespace Wata.MapGenerator {
    public class MapNode: MonoBehaviour, IEnumerable<Vector2Int> {

       //==================================================||Constant
       private const float animationCycle = 0.75f;
       private const float animationScale = 1.2f;
        
       //==================================================||Properties
       public Vector2Int Position { get; private set; }
       public Stage Stage => _stageType;

       public Vector2Int this[Index pIdx] => new (_edges[pIdx].NextNode, Position.y + 1);
       
      //==================================================||Serialize Field 
       [SerializeField] private Image _backGround;
       
       //==================================================||Fields 
       private static Dictionary<Stage, Sprite> mapIcons = null;
        private Stage _stageType;
        private List<(int NextNode, GameObject Edge)> _edges = new();
        private Tween _animation = null;
        
       //==================================================||Methods 

       public void ActiveNode() {
           _animation?.Kill();
           _animation = transform.DOBreathing(animationCycle, animationScale);
       }

       public void ActiveNextNodes() {
           _edges.ForEach(edge => {
               var image = edge.Edge.GetComponent<Image>();
               image.material = MaterialStore.Instance.AbleToMove;
           });
       }
       
        public void Add(int pIdx, GameObject pEdge) =>
            _edges.Add((pIdx, pEdge));
        
        public void Init(Stage pStage, Vector2Int pPosition) {

            FindIcons();
            
            _stageType = pStage;
            _backGround.sprite = mapIcons[pStage];

            Position = pPosition;
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

        public IEnumerator<Vector2Int> GetEnumerator() =>
            _edges
                .Select(edge => new Vector2Int(edge.NextNode, Position.y))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        
       //==================================================||Unity

       private void OnDestroy() {
           _animation?.Kill();
           _edges.ForEach(edge => Destroy(edge.Edge));
       }
    }
}