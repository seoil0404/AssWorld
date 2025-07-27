using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wata.Extension.Test;

namespace Wata.UI.Roullette {
    public class Wheel: MonoBehaviourWrapper {

        private static Dictionary<int, Sprite> _symbolIcon = null;

        private static void SetUp() =>
            _symbolIcon ??= 
                Resources.LoadAll<Sprite>("Symbols")
                    .ToDictionary(
                        sprite => int.Parse(sprite.name[..^2]),
                        sprite => sprite
                    );
    }
}