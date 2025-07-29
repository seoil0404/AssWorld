using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Wata.Extension;
using Wata.Extension.UI;
using Wata.UI.Roulette;

namespace Wata.Data {
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
            var temp = symbols.SelectMany(symbol => symbol);
            var tempPriorities = SymbolManager.Instance
                .Sort(
                    temp
                        .Distinct()
                        .Where(symbol => 
                            SymbolManager.Instance
                                .Symbol(symbol)
                                .Type == SymbolType.Buff 
                        )
                );
            var priorities = new Dictionary<int, int>();

            var idx = -1;
            foreach (var priority in tempPriorities) {
                idx++;
                priorities.Add(priority, idx);
            }

            var orderedDatas = temp
                .Select((symbol, idx) => (
                        Symbol: symbol,
                        Pos: new Vector2Int(
                            idx / PlayerData.Height,
                            idx % PlayerData.Height
                        )
                    )
                )
                .Where(data => 
                    SymbolManager.Instance
                        .Symbol(data.Symbol)
                        .Type == SymbolType.Buff 
                )
                .OrderBy(data => priorities[data.Symbol])
                .ThenBy(data => data.Pos.y)
                .ThenBy(data => data.Pos.x);

            foreach (var data in orderedDatas) {

                var usable = SymbolManager.Instance
                    .Condition(data.Symbol, symbols, data.Pos, _status);
                if (usable) {
                    RouletteManager.Instance.ActiveSymbol(data.Pos);
                }
            }
        }
    }
}