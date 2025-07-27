using System;
using System.Collections.Generic;
using File_wata.Scripts;
using Neeko;
using UnityEngine;
using Wata.Extension;

namespace Wata.UI.Roullette {
    public class RoulletteManager: MonoBehaviour {

        [SerializeField] private Wheel _wheelPrefab;
        [SerializeField] private GameObject _roulletteBoard;

        private List<Wheel> _wheels = new();
        private List<int> _symbols = null;

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
            _symbols = PlayerData.RawSymbols;
            SetUp();
        }
        
        private void Awake() {
        }
    }
}