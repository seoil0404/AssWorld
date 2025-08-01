using UnityEngine;

namespace Neeko {

	[RequireChild("Muzzle")]
	public class GunRotator : MonoBehaviour {

		//======================================================================| Inspector Fields

		[SerializeField]
		private float _rotatingLerpSpeed = 10f;

		//======================================================================| Fields

		private Transform _muzzle;

		//======================================================================| Properties

		public Vector2 Direction => transform.rotation * Vector2.left;

		//======================================================================| Unity Behaviours

		private void Awake() {
			_muzzle = transform.GetChild("Muzzle");
		}

		private void Start() {
			
			if (!Mathf.Approximately(_muzzle.localPosition.y, 0f)) {
				throw new HierarchyException("Muzzle must be horizontal with root gun object.");
			}

		}

		private void Update() {
			
			var mouseInput = Input.mousePosition.WithZ(0);
			var mouseWorld = Camera.main.ScreenToWorldPoint(mouseInput);

			var direction = transform.position - mouseWorld;
			direction.Normalize();

			var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			var currentAngle = transform.eulerAngles.z;

			var lerpFactor = Time.deltaTime * _rotatingLerpSpeed;
			var lerpedAngle = Mathf.LerpAngle(currentAngle, targetAngle, lerpFactor);
			var rotation = Quaternion.Euler(0f, 0f, lerpedAngle);

			transform.rotation = rotation;

		}

	}	

}