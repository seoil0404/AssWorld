using UnityEngine;

namespace Neeko {

	[RequireChild("Muzzle")]
	public class GunRotator : MonoBehaviour {

		//======================================================================| Inspector Fields

		[SerializeField]
		private float _rotatingLerpSpeed;

		//======================================================================| Fields

		private Transform _muzzle;

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

			var direction = mouseWorld - transform.position;
			direction.Normalize();

			var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			var currentAngle = transform.eulerAngles.z;

			Debug.Log($"Target: {targetAngle}");
			Debug.Log($"Current: {currentAngle}");

			var lerpFactor = Time.deltaTime * _rotatingLerpSpeed;
			var lerpedAngle = Mathf.LerpAngle(currentAngle, targetAngle, lerpFactor);
			var rotation = Quaternion.Euler(0f, 0f, lerpedAngle);
			
			Debug.Log($"Lerped: {lerpedAngle}");

			transform.rotation = rotation;

		}

	}	

}