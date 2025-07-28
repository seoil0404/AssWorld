using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Wata.Extension;

namespace Wata.Data {
    public class SymbolManager: MonoSingleton<SymbolManager> {
        protected override bool IsNarrowSingleton { get; set; } = true;

        [SerializeField] private SymbolDataTable _table;

        public SymbolData Symbol(int pSymbol) =>
            _table.Table[pSymbol];
        
       //==================================================||EnumData 
        public static readonly ReadOnlyDictionary<SymbolType, string> SymbolTypeKorean = new(
            new Dictionary<SymbolType, string>() {
                { SymbolType.Buff, "효과"},
                { SymbolType.Skill, "스킬"},
            }
        );
        public static readonly ReadOnlyDictionary<SymbolCategory, string> SymbolCategoryKorean = new(
            new Dictionary<SymbolCategory, string>() {
                {SymbolCategory.Strength, "힘"},
                {SymbolCategory.Dexterity, "민첩"},
                {SymbolCategory.Wisdom, "지혜"},
            }
        );
        public static readonly ReadOnlyDictionary<SymbolRarity, string> SymbolRarityKorean = new(
            new Dictionary<SymbolRarity, string>() {
                { SymbolRarity.Normal, "노말"},
                { SymbolRarity.Rare, "레어"},
                { SymbolRarity.Epic, "에픽"},
                { SymbolRarity.Unique, "유니크"},
                { SymbolRarity.Legendary, "전설"},
            }
        );
        
       //==================================================||Methods 
        public List<int> Sort(List<int> pSymbols) =>
            pSymbols = pSymbols
                .Select(symbol => _table.Table[symbol])
                .OrderBy(symbol => symbol.Type)
                .ThenBy(symbol => symbol.Category)
                .ThenBy(symbol => symbol.ProcessPriority)
                .ThenBy(symbol => symbol.SerialNumber)
                .Select(symbol => symbol.SerialNumber)
                .ToList();
    }
}