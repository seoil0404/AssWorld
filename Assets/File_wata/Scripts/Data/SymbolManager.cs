using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using Wata.Extension;
using Wata.UI.Roulette;
using XLua;

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

        public IEnumerable<int> Sort(IEnumerable<int> pSymbols) =>
            Sort(pSymbols, origin => origin);
       
        public IEnumerable<T> Sort<T>(IEnumerable<T> pSymbols, Func<T, int> pSelector) 
            where T: IComparable =>
            pSymbols = pSymbols
                .Select(data => (
                        Symbol: _table.Table[pSelector.Invoke(data)],
                        Origin: data
                    )
                )
                .OrderBy(data => data.Symbol.ProcessPriority)
                .ThenBy(data => data.Symbol.Type)
                .ThenBy(data => data.Symbol.Category)
                .ThenBy(data => data.Symbol.SerialNumber)
                .ThenBy(data => data.Origin)
                .Select(data => data.Origin)
                .ToList();

        private object[] CallLua(string pCode, string pMethodName, params object[] pArguements) {
            var luaEnv = new LuaEnv();
            var scriptEnv = luaEnv.NewTable();
            var meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            
            luaEnv.Global.Set("StatusManager", CurStatus.Instance);
            luaEnv.Global.Set("Roulette", RouletteManager.Instance);
            
            luaEnv.DoString(pCode);
            var func = scriptEnv.Get<LuaFunction>(pMethodName);
            return func.Call(pArguements);
        }
        
        public bool Condition(int pSymbol, List<List<int>> pBoardInfo, Vector2Int pPos, StatusData pCurStatus) {

            var codeBuilder = new StringBuilder();
            codeBuilder.AppendLine("function Condition(Board, Pos, Status)");
            codeBuilder.AppendLine($"RouletteWidth = {PlayerData.Width};");
            codeBuilder.AppendLine($"RouletteHeight = {PlayerData.Height};");
            codeBuilder.AppendLine(Symbol(pSymbol).ConditionCode);
            codeBuilder.Append("end");
            var code = codeBuilder.ToString();

            return (bool)CallLua(code, "Condition", pBoardInfo, pPos, pCurStatus)[0];
        }

        public Tween Apply(int pSymbol, List<List<int>> pBoardInfo, Vector2Int pPos, StatusData pCurStatus) {
            var codeBuilder = new StringBuilder();
            codeBuilder.AppendLine("function SymbolEffect(Board, Pos, Status)");
            codeBuilder.AppendLine($"RouletteWidth = {PlayerData.Width};");
            codeBuilder.AppendLine($"RouletteHeight = {PlayerData.Height};");
            codeBuilder.AppendLine("AddWisdom = function(...) return StatusManager:AddWisdom(...); end");
            codeBuilder.AppendLine("AddStrength = function(...) return StatusManager:AddStrength(...); end");
            codeBuilder.AppendLine("AddDexterity = function(...) return StatusManager:AddDexterity(...); end");
            codeBuilder.AppendLine("RemoveSymbol = function(...) return Roulette:Remove(...); end");
            codeBuilder.AppendLine(Symbol(pSymbol).Effect);
            codeBuilder.Append("end");
            var code = codeBuilder.ToString();

            return CallLua(code, "SymbolEffect", pBoardInfo, pPos, pCurStatus)[0] as Tween;
        }
    }
}