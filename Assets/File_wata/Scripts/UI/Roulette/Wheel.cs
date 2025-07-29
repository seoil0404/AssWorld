using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Wata.Data;
using Wata.Extension;
using Wata.Extension.Test;
using Random = UnityEngine.Random;

namespace Wata.UI.Roulette {
    public class Wheel: MonoBehaviourWrapper {

        private const float DefaultSpeed = 1750;
        private const float RandomSpeedRange = 500;
        
       //==================================================||Serialize Fields 
        [SerializeField] private SymbolShower _symbolShowerPrefab;
        [SerializeField] private float _speed;
        
       //==================================================||Fields 
        private LinkedList<SymbolShower> _showers = new();
        private bool _isRoll = false;
        
       //==================================================||Properties 
        public bool IsRoll => _isRoll;

        public List<int> Symbols => _showers
            .Skip(1)
            .Select(symbol => symbol.Symbol)
            .ToList();
        
       //==================================================||Methods 

       public void ActiveSymbol(int pY) =>
           _showers.ToList()[pY + 1].Active();
       
       private void SetUp() {

            _isRoll = true;
            _speed = DefaultSpeed + Random.Range(0, RandomSpeedRange);
            
            foreach (var symbolShower in _showers) {
                Destroy(symbolShower.gameObject);   
            }
            _showers.Clear();
            
            var rect = (transform as RectTransform)!;
            var interval = 1f / PlayerData.Height;
            
            
            for (int i = 0; i < PlayerData.Height + 1; i++) {
                var symbol = Instantiate(_symbolShowerPrefab, transform);
                symbol.SetIcon(RouletteManager.Instance.Dequeue());
                
                var symbolRect = (symbol.transform as RectTransform)!;
                symbolRect.SetLocalScale(new(1f, interval));
                symbolRect.SetLocalPosition(new(PivotLocation.Middle, PivotLocation.Up), new(0, -interval * i));
                _showers.AddLast(symbol);    
                
            }
        }

        public void Stop() {
            _isRoll = false;
            
             var idx = -1;
             var height = (transform as RectTransform)!.sizeDelta.y / PlayerData.Height;
             foreach (var shower in _showers) {
                 idx++;
                 if (idx == 0) {
                     shower.gameObject.SetActive(false);
                     continue;
                 }

                 shower.transform.DOLocalMoveY(height * (PlayerData.Height - idx - 0.5f), 0.8f)
                     .SetEase(Ease.OutElastic);
             }
        }
        
       //==================================================||Unity 
        private void Update() {

            if (!_isRoll)
                return;
            
            var cnt = 0;
            var top = _showers.First.Value.transform.position.y;
            var size = (_showers.First.Value.transform as RectTransform)!.sizeDelta.y;
            
            foreach (var shower in _showers) {

                shower.transform.Translate(Vector3.down * Time.deltaTime * _speed);
                var yPos = (shower.transform as RectTransform)!
                    .GetLocalPosition(new (PivotLocation.Middle, PivotLocation.Down)).y;
                
                if (yPos < 0) {
                    cnt++;
                    top += size;
                    var rect = (shower.transform as RectTransform);
                    rect.SetPositionY(top);

                    RouletteManager.Instance.Enqueue(shower.Symbol);
                    shower.SetIcon(RouletteManager.Instance.Dequeue());
                }
            }

            Stack<SymbolShower> temp = new();
            for (int i = 0; i < cnt; i++) {
                temp.Push(_showers.Last.Value);
                _showers.RemoveLast();
            }

            while (temp.Count > 0)
                _showers.AddFirst(temp.Pop());
        }

        private void Start() {
            SetUp();
        }

        private void OnDestroy() {
            foreach (var symbolShower in _showers) {
                Destroy(symbolShower.gameObject);   
            }
        }
    }
}