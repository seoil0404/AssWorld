using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Wata.Extension;
using Wata.Extension.UI;
using Wata.UI.Roulette;

namespace Wata.Data {

    public class SymbolSlotData: IComparable {
        public readonly int Symbol;
        public readonly int X;
        public readonly int Y;
        public Vector2Int Pos => new(X, Y);
        
        public SymbolSlotData(int pSymbol, int pX, int pY) =>
            (Symbol, X, Y) = (pSymbol, pX, pY);
        
        public int CompareTo(object obj) {
            if (obj is not SymbolSlotData test)
                return 0;

            var result = Y.CompareTo(test.Y);
            if (result == 0)
                result = X.CompareTo(test.X);

            return result;
        }
    }
    
    public class CurStatus: MonoSingleton<CurStatus> {
        protected override bool IsNarrowSingleton { get; set; } = true;

        [SerializeField] private TMP_Text _strength;
        [SerializeField] private TMP_Text _dexterity;
        [SerializeField] private TMP_Text _wisdom;
        
        [SerializeField] private CustomSlider _hp;
        [SerializeField] private TMP_Text _money;

        private StatusData _status;

        public void AddStrength(int pAmount) {
            _status.Strength += pAmount;
            
        }
        
        public void Apply() {

            _status = new();
            
            var symbols = RouletteManager.Instance.RouletteSymbols;
            var temp = symbols
                .SelectMany(symbol => symbol)
                .Select((symbol, idx) => 
                    new SymbolSlotData(
                        symbol,
                        idx / PlayerData.Height,
                        idx % PlayerData.Height
                    )
                )
                .Where(data => 
                    SymbolManager.Instance.Symbol(data.Symbol).Type == SymbolType.Buff
                );
            
            var result = SymbolManager.Instance
                .Sort(temp, origin => origin.Symbol);

            foreach (var data in result) {

                var usable = SymbolManager.Instance
                    .Condition(data.Symbol, symbols, data.Pos, _status);
                if (usable) {
                    RouletteManager.Instance.ActiveSymbol(data.Pos);
                }
            }
        }
    }
}