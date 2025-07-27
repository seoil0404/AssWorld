using UnityEngine;
using Wata.Extension;

namespace Wata.MapGenerator {
    public class EdgeMaterial: MonoSingleton<EdgeMaterial> {
        
        protected override bool IsNarrowSingleton { get; set; } = true;

        [field: SerializeField] public Material _canNotMove { get; private set; }
        [field: SerializeField] public Material _alreadyVisit { get; private set; }
        [field: SerializeField] public Material _ableToMove { get; private set; }
    }
}