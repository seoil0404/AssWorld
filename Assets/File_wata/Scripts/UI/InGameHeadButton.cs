using UnityEngine;
using UnityEngine.UI;

namespace File_wata.Scripts.UI {
    public class InGameHeadButton: MonoBehaviour {
        
        [Header("Map")]
        [SerializeField] private Image _map;
        private bool _isMapActive = false;

        public void MapButton() {

            _isMapActive = !_isMapActive;
            _map.gameObject.SetActive(_isMapActive);
        }
    }
}