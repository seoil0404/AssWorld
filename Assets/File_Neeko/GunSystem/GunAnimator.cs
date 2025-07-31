using System.Collections.Generic;
using UnityEngine;

namespace Neeko {

	public class GunAnimator : MonoBehaviour {

		//======================================================================| Fields

		private IEnumerable<GunShootAnimateTrigger> _animators;

		//======================================================================| Unity Behaviours

		private void Awake() {
			_animators = GetComponentsInChildren<GunShootAnimateTrigger>();
		}

		//======================================================================| Methods

		public void OnShoot() {
			foreach (var animator in _animators) {
				animator.OnShoot();
			}
		}

	}

}