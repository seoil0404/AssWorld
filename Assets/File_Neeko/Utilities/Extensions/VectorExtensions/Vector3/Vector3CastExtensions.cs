using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Vector3CastExtensions {
	
		public static Vector2 ToVector2(this Vector3 vector3) => vector3;
		public static Vector4 ToVector4(this Vector3 vector3, float w = 0f) => new(vector3.x, vector3.y, vector3.z, w);
		public static Vector2Int ToVector2Int(this Vector3 vector3) => new((int)vector3.x, (int)vector3.y);
		public static Vector3Int ToVector3Int(this Vector3 vector3) => new((int)vector3.x, (int)vector3.y, (int)vector3.z);

	}
}
