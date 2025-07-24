using UnityEngine;

namespace Neeko {
	
	[RequireComponent(typeof(Rigidbody2D))]
	public class GunBehaviours : MonoBehaviour {

		//======================================================================| Fields

		private Rigidbody2D _rigidbody;

		//======================================================================| Unity Behaviours

		private void Awake() {
			_rigidbody = GetComponent<Rigidbody2D>();
		}


	}

}