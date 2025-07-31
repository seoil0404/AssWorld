using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using Wata.Data;
using Wata.Extension;
using Wata.Extension.Test;
using XLua;

namespace Wata.UI.Roulette {
    public class RouletteManager: MonoSingleton<RouletteManager> {
        
        //==================================================||Serialize Fields
        [SerializeField] private Wheel _wheelPrefab;
        [SerializeField] private GameObject _rouletteBoard;

        [Space] 
        [Header("Animation")] 
        [SerializeField] private Animator _animator;
        //==================================================||Fields
        private List<Wheel> _wheels = new();
        private Queue<int> _symbolsQueue = new();
        private Tween _shakeAnimation = null;
        private Vector2 _initialPos = Vector2.zero;

        //==================================================||Properties 
        public bool IsRoll { get; private set; } = false;
        public List<List<int>> RouletteSymbols => _wheels
            .Select(symbol => symbol.Symbols)
            .ToList();
        protected override bool IsNarrowSingleton { get; set; } = true;
        
        //==================================================||Methods 

        public void ActiveSymbol(Vector2Int pPos) =>
            _wheels[pPos.x].ActiveSymbol(pPos.y);
        
        public void Enqueue(int pSymbol) =>
            _symbolsQueue.Enqueue(pSymbol);

        public int Dequeue() =>
            _symbolsQueue.Dequeue();
        
        private void SetUp() {
            foreach (var wheel in _wheels) {
                Destroy(wheel.gameObject);
            }
            _wheels.Clear();

            var interval = 1f / PlayerData.Width;
            for (int i = 0; i < PlayerData.Width; i++) {
                var wheel = Instantiate(_wheelPrefab, _rouletteBoard.transform);
                _wheels.Add(wheel);

                var rect = (wheel.transform as RectTransform)!;
                rect.SetLocalPosition(new(PivotLocation.Down), new((i + 0.5f) * interval, 0));
                rect.SetLocalScale(new(interval, 1));
            }
        }

        private void StartRoll() {

            //animation
            _initialPos = _rouletteBoard.transform.position;
            _shakeAnimation = 
                _rouletteBoard.transform.DOShakePosition(1f, new Vector2(0, 5), fadeOut: false)
                    .SetLoops(-1);

            IsRoll = true;
            _symbolsQueue.Clear();
            foreach(var symbol in PlayerData.RawSymbols.Shuffle()) {
                _symbolsQueue.Enqueue(symbol);
            }
            SetUp();
        }

        public void Stop() {

            if (!IsRoll)
                return;
            
            _animator.Play("LabberDown");
            
            foreach (var wheel in _wheels) {
                if (wheel.IsRoll) {
                    
                    wheel.Stop();
                    break;
                }
            }

            IsRoll = _wheels.Any(wheel => wheel.IsRoll);
            if (!IsRoll) {
                _shakeAnimation?.Kill();
                _rouletteBoard.transform.position = _initialPos;
                
                CurStatus.Instance.Apply();
            }
        }
        
        //==================================================||Unity 
        private new void Awake() {
            base.Awake();
            
            PlayerData.Init();
            PlayerData.AddSymbol(1001, 3);
            PlayerData.AddSymbol(1002, 5);
            PlayerData.AddSymbol(1003, 3);
            PlayerData.AddSymbol(1004, 5);
            PlayerData.AddSymbol(1005, 1);
            PlayerData.AddSymbol(1006, 1);
            PlayerData.AddSymbol(1007, 1);
            PlayerData.AddSymbol(1008, 1);
            
            StartRoll();
        }
    }
}