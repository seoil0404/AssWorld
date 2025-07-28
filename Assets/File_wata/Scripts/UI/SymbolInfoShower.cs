using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wata.Data;
using Wata.Extension;

namespace Wata.UI {
    public class SymbolInfoShower: MonoSingleton<SymbolInfoShower> {

        [SerializeField] private GameObject _shower;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _condition;

        private static readonly Vector2 AnimationScale = new(0.95f, 0.95f);
        private const float AnimationDuration = 0.2f;
        private const string NameFormat = "이름: {0}"; 
        private const string DescriptionFormat = "분류: {0},\t종류: {1}\n설명: {2}"; 
        private const string ConditionFormat = "조건: {0}";

        private Tween _animation = null;
        private bool _isActive = false;
        
        public void TurnOn(int pSymbol, bool pConditionCheek = false) {

            _isActive = true;
            Update();
            
            _shower.SetActive(true);
            _shower.transform.localScale = AnimationScale;
            _animation = _shower.transform.DOScale(Vector3.one, AnimationDuration);   
            
            var symbol = SymbolManager.Instance.Symbol(pSymbol);
            _name.text = string.Format(NameFormat, symbol.Name);

            var type = SymbolManager.SymbolTypeKorean[symbol.Type];
            var category = SymbolManager.SymbolCategoryKorean[symbol.Category];
            _description.text = string.Format(DescriptionFormat, type, category, symbol.Description);

            _condition.text = string.Format(ConditionFormat, symbol.Condition);
            
            if (pConditionCheek) {
                //TODO: Check condition
            }
        }

        public void TurnOff() {
            _isActive = false;
            _animation?.Kill();
            _shower.SetActive(false);           
        }

        private new void Awake() {
            base.Awake();
            _shower.SetActive(false);
        }

        private new void Update() {
            base.Update();

            if (_isActive)
                _shower.transform.position = Input.mousePosition;
        }
        
        protected override bool IsNarrowSingleton { get; set; } = true;
    }
}