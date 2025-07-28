using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wata.Data;
using Wata.Extension;

namespace Wata.SymbolInventory {
    public class SymbolInventory: MonoBehaviour {

        private const int Width = 3;
        private const int Height = 9;

        [SerializeField] private SymbolInventorySlot _inventorySlotPrefab;
        [SerializeField] private GameObject _box;
        private List<SymbolInventorySlot> _slots;
        
        private void Show() {

            var symbols = SymbolManager.Instance.Sort(
                PlayerData.Symbols
                    .Select(symbol => symbol.Item)
                    .ToList()
            );

            var idx = -1;
            var widthInterval = 1f / (Width + 1);
            var heightInterval = 1f / (Height + 1);
            
            foreach (var symbol in symbols) {

                idx++;
                var slot = Instantiate(_inventorySlotPrefab, _box.transform);
                slot.SetUp(symbol);

                var rect = (slot.transform as RectTransform)!;
                var x = idx / Height + 1;
                var y = idx % Height + 1;
                
                rect.SetLocalPosition(
                    new(PivotLocation.Down, PivotLocation.Up), 
                    new(widthInterval * x, -heightInterval * y)
                );
            }
        }

        private void Start() {
            Show();
        }
    }
}