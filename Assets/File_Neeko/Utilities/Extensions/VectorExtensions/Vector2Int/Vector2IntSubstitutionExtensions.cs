using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Vector2IntSubstitutionExtensions {

		public static Vector2Int WithX(this Vector2Int vector2Int, int x) => new(x, vector2Int.y);
		public static Vector2Int WithY(this Vector2Int vector2Int, int y) => new(vector2Int.x, y);

	}
}
