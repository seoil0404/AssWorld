using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Wata.Extension;
using Wata.Extension.Test;
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

        private StatusData _status = new();

        public Tween AddStrength(int pAmount) {
            var temp = _status.Strength;
            _status.Strength += pAmount;

            return AddAnimation(_strength, temp, _status.Strength);
        }

        [TestMethod]
        public Tween AddDexterity(int pAmount) {
            var temp = _status.Dexterity;
            _status.Dexterity += pAmount;

            return AddAnimation(_dexterity, temp, _status.Dexterity);
        }
        
        public Tween AddWisdom(int pAmount) {
            var temp = _status.Wisdom;
            _status.Wisdom += pAmount;

            return AddAnimation(_wisdom, temp, _status.Wisdom);
        }


        private Tween AddAnimation(TMP_Text pTarget, int pStart, int pEnd) =>
            DOTween.Sequence()
                .Append(pTarget.transform.DOShakePosition(0.65f,
                    4 * Mathf.Log(Mathf.Abs(pEnd - pStart) + 1) * Vector3.up))
                .Join(pTarget.DOCounter(pStart, pEnd, 0.5f))
                .Join(_dexterity.DOFontSize(60, 0.3f).SetEase(Ease.OutCirc))
                .Append(_dexterity.DOFontSize(40, 0.3f).SetEase(Ease.OutQuad));

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
                    SymbolManager.Instance.Apply(data.Symbol, symbols, data.Pos, _status);
                    RouletteManager.Instance.ActiveSymbol(data.Pos);
                }
            }
        }
    }
}