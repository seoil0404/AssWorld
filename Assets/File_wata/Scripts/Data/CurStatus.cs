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

        public StatusData Status { get; private set; } = new();

        public Tween AddStrength(int pAmount) {
            var temp = Status.Strength;
            Status.Strength += pAmount;

            return AddAnimation(_strength, temp, Status.Strength);
        }

        public Tween AddDexterity(int pAmount) {
            var temp = Status.Dexterity;
            Status.Dexterity += pAmount;

            return AddAnimation(_dexterity, temp, Status.Dexterity);
        }
        
        public Tween AddWisdom(int pAmount) {
            var temp = Status.Wisdom;
            Status.Wisdom += pAmount;

            return AddAnimation(_wisdom, temp, Status.Wisdom);
        }


        private Tween AddAnimation(TMP_Text pTarget, int pStart, int pEnd) =>
            DOTween.Sequence()
                .Append(pTarget.transform.DOShakePosition(0.4f,
                    4 * Mathf.Log(Mathf.Abs(pEnd - pStart) + 1) * Vector3.up))
                .Join(pTarget.DOCounter(pStart, pEnd, 0.35f))
                .Join(pTarget.DOFontSize(60, 0.2f).SetEase(Ease.OutCirc))
                .Append(pTarget.DOFontSize(40, 0.2f).SetEase(Ease.OutQuad));

        public Queue<SymbolSlotData> Apply() {

            Status = new();
            
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
            
            var candidate = SymbolManager.Instance
                .Sort(temp, origin => origin.Symbol);

            Queue<SymbolSlotData> result = new();
            foreach (var data in candidate) {

                var usable = SymbolManager.Instance
                    .Condition(data.Symbol, symbols, data.Pos, Status);
                if (usable) {
                    result.Enqueue(data);
                }
            }

            return result;
        }
    }
}