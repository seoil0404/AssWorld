using System.Collections;
using UnityEngine;

namespace Neeko {
	
	[RequireComponent(typeof(GunRotator))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class GunController : MonoBehaviour {

		//======================================================================| Inspector Fields

		[SerializeField]
		private GameObject _projectile;

		[SerializeField]
		private float _reloadingDuration;

		[SerializeField]
		private Per<Second> _sootingRate;

		[SerializeField]
		private float _recoil;

		//======================================================================| Fields

		private Rigidbody2D _rigidbody;

		private float _colldown = 0f;

		private bool _isTriggered = false;
		private bool _isTimeFrezzed = false;

		//======================================================================| Unity Behaviours

		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			
			GetInput();
			ShootDetection();

		}

		//======================================================================| Methods

		private void GetInput() {

			_isTriggered = Input.GetKey(KeyCode.Mouse0);
			_isTimeFrezzed = Input.GetKey(KeyCode.Mouse1);

		}

		private void ShootDetection() {
		
			_colldown -= Time.deltaTime;

			if (_isTriggered && _colldown <= 0f) {
				_colldown = _sootingRate.Interval;
				Shoot();
			}

		}

		private void Shoot() {

			

		}

	}

}