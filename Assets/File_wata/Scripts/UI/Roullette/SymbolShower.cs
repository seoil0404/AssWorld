using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Wata.Extension;

namespace Wata.UI.Roullette {
    
    [RequireComponent(typeof(Image))]
    public class SymbolShower: MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _size;
        private static Dictionary<int, Sprite> _symbolIcon = null;
        private int _symbol;
       
        
        public void SetIcon(int pSymbol) {
            
            _image.rectTransform.SetLocalPosition(new Pivot(y: PivotLocation.Down));
            _image.rectTransform.SetLocalScale(_size);
            _symbol = pSymbol;
            _image.sprite = _symbolIcon[pSymbol];
        }
       
        private static void SetUp() =>
            _symbolIcon ??= 
                Resources.LoadAll<Sprite>("Symbols")
                    .ToDictionary(
                        sprite => int.Parse(sprite.name[..^2]),
                        sprite => sprite
                    );

        private void Awake() {
            SetUp();
        }
    }
}