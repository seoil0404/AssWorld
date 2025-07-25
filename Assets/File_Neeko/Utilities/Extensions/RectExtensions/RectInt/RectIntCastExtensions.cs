using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RectIntCastExtensions {

		public static Rect ToRect(this RectInt rectInt) => new(
			rectInt.position,
			rectInt.size
		);

	}
}
