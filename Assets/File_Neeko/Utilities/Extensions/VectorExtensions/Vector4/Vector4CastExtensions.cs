using UnityEngine;

namespace Neeko {

	public static class Vector4CastExtensions {

		public static Vector2 ToVector2(this Vector4 vector4) => vector4;
		public static Vector3 ToVector3(this Vector4 vector4) => vector4;
		public static Vector2Int ToVector2Int(this Vector4 vector4) => new((int)vector4.x, (int)vector4.y);
		public static Vector3Int ToVector3Int(this Vector4 vector4) => new((int)vector4.x, (int)vector4.y, (int)vector4.z);

	}
}
