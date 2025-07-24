using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Vector2SubstitutionExtensions {

		public static Vector2 WithX(this Vector2 vector2, float x) => new(x, vector2.y);
		public static Vector2 WithY(this Vector2 vector2, float y) => new(vector2.x, y);

	}
}
