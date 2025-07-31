using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Wata.Data;
using Wata.UI;

namespace Wata.SymbolInventory {
    public class SymbolInventorySlot: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _amount;
        private int _symbol;  
        
        public void SetUp(int pSymbol) {
            _symbol = pSymbol;
            _image.sprite = _symbol.GetIcon();
            _amount.text = $"x{_symbol.Amount()}";
        }

        public void OnPointerEnter(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOn(_symbol);
        }

        public void OnPointerExit(PointerEventData eventData) {
            SymbolInfoShower.Instance.TurnOff();
        }
    }
}