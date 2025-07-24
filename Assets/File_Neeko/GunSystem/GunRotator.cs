using UnityEngine;

namespace Neeko {

	[RequireChild("Muzzle")]
	public class GunRotator : MonoBehaviour {

		//======================================================================| Fields

		[SerializeField]
		private float _rotatingLerpSmoothness = 0f;

		//======================================================================| Unity Behaviours

		private void Update() {
			
			var mouseInput = Input.mousePosition.WithZ(0);
			var mouseWorld = Camera.main.ScreenToWorldPoint(mouseInput);

			// 계산 잘못함 자살하고 싶다

		}

	}	

}