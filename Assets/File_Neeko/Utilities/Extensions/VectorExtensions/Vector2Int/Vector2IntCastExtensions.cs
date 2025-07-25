using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Vector2IntCastExtensions {

		public static Vector2 ToVector2(this Vector2Int vector2Int) => vector2Int;
		public static Vector3 ToVector3(this Vector2Int vector2Int, float z = 0f) => new(vector2Int.x, vector2Int.y, z);
		public static Vector4 ToVector4(this Vector2Int vector2Int, float z = 0f, float w = 0f) => new(vector2Int.x, vector2Int.y, z, w);
		public static Vector3Int ToVector2Int(this Vector2Int vector2Int, int z = 0) => new(vector2Int.x, vector2Int.y, z);

	}
}
