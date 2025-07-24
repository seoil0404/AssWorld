using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector2CastExtensions { 

	public static Vector3 ToVector3(this Vector2 vector2, float z = 0f) => new(vector2.x, vector2.y, z);
	public static Vector4 ToVector4(this Vector2 vector2, float z = 0f, float w = 0f) => new(vector2.x, vector2.y, z, w);
	public static Vector2Int ToVector2Int(this Vector2 vector2) => new((int)vector2.x, (int)vector2.y);
	public static Vector3Int ToVector3Int(this Vector2 vector2, int z = 0) => new((int)vector2.x, (int)vector2.y, z);

}
