using System;
using System.Collections.Generic;
using System.Linq;
using File_wata.Scripts;
using UnityEngine;
using Wata.Extension;
using Wata.Extension.Test;

namespace Wata.UI.Roullette {
    public class Wheel: MonoBehaviourWrapper {

        [SerializeField] private SymbolShower _symbolShowerPrefab;

        private void SetUp() {

            var rect = (transform as RectTransform)!;
            var interval = 1f / PlayerData.Height;
            
            for (int i = 0; i < PlayerData.Height + 1; i++) {
                var symbol = Instantiate(_symbolShowerPrefab, transform);
                symbol.SetIcon(RouletteManager.Instance.Dequeue());
                
                var symbolRect = (symbol.transform as RectTransform)!;
                symbolRect.SetLocalScale(new(1f, interval));
                symbolRect.SetLocalPosition(new(PivotLocation.Middle, PivotLocation.Up), new(0, -interval * i));
            }
        }

        private void Start() {
            SetUp();
        }
    }
}