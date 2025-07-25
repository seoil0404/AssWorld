using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Vector3IntCastExtensions {

		public static Vector2 ToVector2(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y);
		public static Vector3 ToVector3(this Vector3Int vector3Int) => vector3Int;
		public static Vector4 ToVector4(this Vector3Int vector3Int, float w = 0f) => new(vector3Int.x, vector3Int.y, vector3Int.z, w);
		public static Vector2Int ToVector2Int(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y);

	}
}
