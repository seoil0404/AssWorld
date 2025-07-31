using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wata.Data {
    public static class PlayerData {

        public const int Width = 5;
        public const int Height = 3;

        public const int MaxSymbolCount = 25;

        private static SortedDictionary<int, int> symbols = new();

       //==================================================||Properties 
       
        public static List<(int Item, int Count)> Symbols {
            get {

                var removeTargets = symbols.Where(items => items.Value <= 0 && items.Key != 0);
                foreach (var removeTarget in removeTargets) {
                    symbols.Remove(removeTarget.Key);
                }
                
                return symbols
                    .Select(kvp => (kvp.Key, kvp.Value))
                    .ToList();
            }
    }

        public static List<int> RawSymbols {
            get {
                var result = new List<int>();
                foreach (var symbol in Symbols) {
                    for (int i = 0; i < symbol.Count; i++) {
                        result.Add(symbol.Item);   
                    }
                }

                return result;
            } 
        }
           
        
        //==================================================||Methods
        public static void Init() {
            symbols.Clear();
            symbols.Add(0, MaxSymbolCount);
        }

        public static int Amount(this int pSymbol) {
            if (symbols.TryGetValue(pSymbol, out var result))
                return result;
            return 0;
        }
        
        public static bool IsAddable(out int pNecessarySpace, int pAmount = 1) {

            pNecessarySpace = pAmount - symbols[0];
            return symbols[0] >= pAmount;
        }
        
        public static void AddSymbol(int pSymbol, int pAmount = 1) {
            
            if (!IsAddable(out var necessarySpace ,pAmount)) 
                throw new Exception($"You can't add item, You should remove items.(need space: {necessarySpace}");

            symbols[0] -= pAmount;
            
            symbols.TryAdd(pSymbol, 0);
            symbols[pSymbol] += pAmount;
            Debug.Log($"{pSymbol} is added(count: {pAmount})");
        }

        public static void RemoveSymbol(int pSymbol, int pAmount = 1) {

            if (symbols[pSymbol] < pAmount)
                throw new ArgumentException($"Can't remove item({pSymbol}, Count: {symbols[pSymbol]}, TargetCount: {pAmount})");

            symbols[0] += pAmount;
            symbols[pSymbol] -= pAmount;
            Debug.Log($"{pSymbol} is removed(count: {pAmount})");
        }
    }
}