using UnityEngine;

namespace Neeko {
	
	[RequireComponent(typeof(Rigidbody2D))]

	[RequireComponent(typeof(GunRotator))]
	[RequireComponent(typeof(GunAnimator))]

	public class GunController : MonoBehaviour {

		//======================================================================| Inspector Fields

		[SerializeField]
		private GameObject _projectile;

		[SerializeField]
		private bool _isRapidFire;

		[SerializeField]
		private float _reloadingDuration;

		[SerializeField]
		private Per<Second> _shootingRate;

		[SerializeField]
		private float _recoil;

		//======================================================================| Fields

		private Rigidbody2D _rigidbody;

		private GunRotator _rotator;
		private GunAnimator _animator;

		private float _colldown = 0f;

		private bool _isTriggered = false;
		private bool _isTimeFrezzed = false;

		//======================================================================| Unity Behaviours

		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
			_rotator = GetComponent<GunRotator>();
			_animator = GetComponent<GunAnimator>();
		}

		private void Update() {
			
			GetInput();
			ShootProcess();

		}

		//======================================================================| Methods

		private void GetInput() {

			_isTimeFrezzed = Input.GetKey(KeyCode.Mouse1);

			_isTriggered = _isRapidFire
				? Input.GetKey(KeyCode.Mouse0)
				: Input.GetKeyDown(KeyCode.Mouse0);

		}

		private void ShootProcess() {
		
			_colldown -= Time.deltaTime;

			if (_isTriggered && _colldown <= 0f) {
				_colldown = _shootingRate.Interval;
				Shoot();
			}

		}

		private void Shoot() {

			var direction = -_rotator.Direction;
			var force = direction * _recoil;

			_rigidbody.linearVelocity = force;

			_animator.OnShoot();

		}

	}

}