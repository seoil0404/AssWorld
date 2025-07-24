using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector3SubstitutionExtensions {

	public static Vector3 WithX(this Vector3 vector3, float x) => new(x, vector3.y, vector3.z);
	public static Vector3 WithY(this Vector3 vector3, float y) => new(vector3.x, y, vector3.z);
	public static Vector3 WithZ(this Vector3 vector3, float z) => new(vector3.x, vector3.y, z);

}