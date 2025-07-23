using Wata.Extension.Serialize;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wata.Extension.UI {
    public class CustomSlider: MonoBehaviour {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TMP_Text _progress;

        [Space, Header("ProgressValue")]
        [SerializeField, WhenValueChange("Fill")] 
        private float _value;
        
        public float Value {
            get => _value;
            set {
                _value = value;
                Fill();
            }
        }

        private void Fill() {
            _value = Mathf.Clamp01(_value);
            _progressBar.fillAmount = _value;
            _progress.text = $"{(int)(_value * 100f)}%";
        }
    }
}