using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Wata.Data;
using Wata.Extension;
using Wata.MapGenerator;

namespace Wata.UI.Roulette {
    
    [RequireComponent(typeof(Image))]
    public class SymbolShower: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image _image;
        [SerializeField] private Vector2 _size;
        private int _symbol;
        public int Symbol => _symbol;

        public Tween Active() =>
            _image.transform.DOScale(1.2f, 0.5f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _image.material = MaterialStore.Instance.Gray);
        
        public void SetIcon(int pSymbol) {
            
            _image.rectTransform.SetLocalPosition(new Pivot(y: PivotLocation.Down));
            _image.rectTransform.SetLocalScale(_size);
            _symbol = pSymbol;

            _image.sprite = pSymbol.GetIcon();
        }

        public void Remove() {
            PlayerData.RemoveSymbol(_symbol);
            SetIcon(0);
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOn(_symbol);
        }

        public void OnPointerExit(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOff();
        }
    }
}