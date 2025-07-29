using UnityEngine;
using Wata.Extension;

namespace Wata.MapGenerator {
    public class MaterialStore: MonoSingleton<MaterialStore> {
        
        protected override bool IsNarrowSingleton { get; set; } = true;

        [field: SerializeField] public Material CanNotMove { get; private set; }
        [field: SerializeField] public Material AlreadyVisit { get; private set; }
        [field: SerializeField] public Material AbleToMove { get; private set; }
        
        [field: SerializeField] public Material Gray { get; private set; }
    }
}