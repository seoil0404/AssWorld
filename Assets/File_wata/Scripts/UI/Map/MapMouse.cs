
using UnityEngine;

namespace Wata.UI.Map {
    public class MapMouse: MonoBehaviour {

        private const float _power = -50;
        
        private void Update() {
            var delta = Input.mouseScrollDelta.y;
            var pos = transform.position;
            pos.y += _power * delta;

            transform.position = pos;
        }
        
    }
}