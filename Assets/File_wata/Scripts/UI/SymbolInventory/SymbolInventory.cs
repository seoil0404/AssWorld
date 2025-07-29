using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Wata.Data;
using Wata.Extension;

namespace Wata.SymbolInventory {
    public class SymbolInventory: MonoBehaviour {

        private const int Width = 3;
        private const int Height = 9;

        [SerializeField] private SymbolInventorySlot _inventorySlotPrefab;
        [SerializeField] private GameObject _box;
        private List<SymbolInventorySlot> _slots = new();
        public bool NeedUpdate { get; set; } = false;
        private bool _isOn = false;
        
        private void UpdateSlots() {

            foreach (var slot in _slots) {
                Destroy(slot.gameObject);
            }
            _slots.Clear();
            
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
                    Pivot.Middle,
                    new(widthInterval * x, -heightInterval * y)
                );
                
                _slots.Add(slot);
            }
        }

       //==================================================||Animation 
        public void Switch() {
         
            if(_isOn)
                TurnOff();
            else 
                TurnOn();
        }

        private void TurnOn() {

            _isOn = true;
            _box.transform.DOMoveX(0, 0.5f)
                .SetEase(Ease.OutBounce);
        }
        
        private void TurnOff() {
            
            _isOn = false;
            _box.transform.DOMoveX(-(_box.transform as RectTransform)!.sizeDelta.x, 0.1f);
        }
        
       //==================================================||Unity 
        private void Awake() {
            NeedUpdate = true;
        }

        private void Update() {
            if (NeedUpdate) {
                
                UpdateSlots();
                NeedUpdate = false;
            }
        }
    }
}