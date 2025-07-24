using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector3RoundingExtensions {

	public static Vector3 Round(this Vector3 vector3) => new(
		Mathf.Round(vector3.x),
		Mathf.Round(vector3.y),
		Mathf.Round(vector3.z)
	);

	public static Vector3 Ceil(this Vector3 vector3) => new(
		Mathf.Ceil(vector3.x),
		Mathf.Ceil(vector3.y),
		Mathf.Ceil(vector3.z)
	);

	public static Vector3 Floor(this Vector3 vector3) => new(
		Mathf.Floor(vector3.x),
		Mathf.Floor(vector3.y),
		Mathf.Floor(vector3.z)
	);

	public static Vector3Int RoundToInt(this Vector3 vector3) => new(
		Mathf.RoundToInt(vector3.x),
		Mathf.RoundToInt(vector3.y),
		Mathf.RoundToInt(vector3.z)
	);

	public static Vector3Int CeilToInt(this Vector3 vector3) => new(
		Mathf.CeilToInt(vector3.x),
		Mathf.CeilToInt(vector3.y),
		Mathf.CeilToInt(vector3.z)
	);

	public static Vector3Int FloorToInt(this Vector3 vector3) => new(
		Mathf.FloorToInt(vector3.x),
		Mathf.FloorToInt(vector3.y),
		Mathf.FloorToInt(vector3.z)
	);

} 