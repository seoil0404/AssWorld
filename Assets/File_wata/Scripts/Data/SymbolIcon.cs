using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wata.Data {
    public static class SymbolIcon {
        
        private static Dictionary<int, Sprite> _symbolIcon = null;
        private static void SetUp() =>
            _symbolIcon ??= 
                Resources.LoadAll<Sprite>("Symbols")
                    .ToDictionary(
                        sprite => int.Parse(sprite.name[..^2]),
                        sprite => sprite
                    );

        public static Sprite GetIcon(this int pSymbol) {
            SetUp();
            
            if (_symbolIcon.TryGetValue(pSymbol, out var result))
                return result;

            return _symbolIcon[-1];
        } 
    }
}