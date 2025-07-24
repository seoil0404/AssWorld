using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector4SubstitutionExtensions {

	public static Vector4 WithX(this Vector4 vector4, int x) => new(x, vector4.y, vector4.z, vector4.w);
	public static Vector4 WithY(this Vector4 vector4, int y) => new(vector4.x, y, vector4.z, vector4.w);
	public static Vector4 WithZ(this Vector4 vector4, int z) => new(vector4.x, vector4.y, z, vector4.w);
	public static Vector4 WithW(this Vector4 vector4, int w) => new(vector4.x, vector4.y, vector4.z, w);

}