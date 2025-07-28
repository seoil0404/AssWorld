using System;
using System.Collections.Generic;
using System.Linq;
using File_wata.Scripts;
using UnityEngine;
using Wata.Extension;
using Wata.Extension.Test;
using Random = UnityEngine.Random;

namespace Wata.UI.Roullette {
    public class Wheel: MonoBehaviourWrapper {

        private const float DefaultSpeed = 1200;
        private const float RandomSpeedRange = 500;
        
        [SerializeField] private SymbolShower _symbolShowerPrefab;
        [SerializeField] private float _speed;
        private LinkedList<SymbolShower> _showers = new();
        
        private void SetUp() {

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

        private void Update() {

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