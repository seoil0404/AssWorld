using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Wata.Data;
using Wata.Extension;

namespace Wata.UI.Roulette {
    
    [RequireComponent(typeof(Image))]
    public class SymbolShower: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _size;
        private int _symbol;
        public int Symbol => _symbol;
        
        public void SetIcon(int pSymbol) {
            
            _image.rectTransform.SetLocalPosition(new Pivot(y: PivotLocation.Down));
            _image.rectTransform.SetLocalScale(_size);
            _symbol = pSymbol;

            _image.sprite = pSymbol.GetIcon();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOn(_symbol);
        }

        public void OnPointerExit(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOff();
        }
    }
}