using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector3IntSubstitutionExtensions {

	public static Vector3Int WithX(this Vector3Int vector3Int, int x) => new(x, vector3Int.y, vector3Int.z);
	public static Vector3Int WithY(this Vector3Int vector3Int, int y) => new(vector3Int.x, y, vector3Int.z);
	public static Vector3Int WithZ(this Vector3Int vector3Int, int z) => new(vector3Int.x, vector3Int.y, z);

}