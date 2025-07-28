using System;
using System.Collections.Generic;
using File_wata.Scripts;
using Neeko;
using UnityEngine;
using Wata.Extension;

namespace Wata.UI.Roullette {
    public class RouletteManager: MonoSingleton<RouletteManager> {

        protected override bool IsNarrowSingleton { get; set; } = true;
        
        [SerializeField] private Wheel _wheelPrefab;
        [SerializeField] private GameObject _roulletteBoard;

        private List<Wheel> _wheels = new();
        private Queue<int> _symbols = new();

        public void Enqueue(int pSymbol) =>
            _symbols.Enqueue(pSymbol);

        public int Dequeue() =>
            _symbols.Dequeue();
        
        private void SetUp() {
            foreach (var wheel in _wheels) {
                Destroy(wheel.gameObject);
            }
            _wheels.Clear();

            var interval = 1f / PlayerData.Width;
            for (int i = 0; i < PlayerData.Width; i++) {
                var wheel = Instantiate(_wheelPrefab, _roulletteBoard.transform);
                _wheels.Add(wheel);

                var rect = (wheel.transform as RectTransform)!;
                rect.SetLocalPosition(new(PivotLocation.Down), new((i + 0.5f) * interval, 0));
                rect.SetLocalScale(new(interval, 1));
            }
        }

        private void StartRoll() {

            _symbols.Clear();
            foreach(var symbol in PlayerData.RawSymbols.Shuffle()) {
                _symbols.Enqueue(symbol);
            }
            SetUp();
        }

        private new void Awake() {
            base.Awake();
            
            PlayerData.Init();
            PlayerData.AddSymbol(1001, 3);
            PlayerData.AddSymbol(1002, 5);
            PlayerData.AddSymbol(1003, 3);
            PlayerData.AddSymbol(1004, 7);
            PlayerData.AddSymbol(1005, 1);
            PlayerData.AddSymbol(1006, 1);
            
            StartRoll();
        }
    }
}