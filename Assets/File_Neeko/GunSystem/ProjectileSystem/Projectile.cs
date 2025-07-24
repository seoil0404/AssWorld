using UnityEngine;

namespace Neeko {

	public abstract class Projectile : MonoBehaviour {

		//======================================================================| Inspector Fields

		#pragma warning disable

		[SerializeField]
		private float _initialSpeed = 1f;

		[SerializeField]
		private float _bulletSpread = 0f;

		#pragma warning restore

		//======================================================================| Methods

		public virtual void OnShoot() {}
		public virtual void OnUpdate() {}
		public virtual void OnHit() {}
		public virtual void OnDestroyed () {}

	}

}