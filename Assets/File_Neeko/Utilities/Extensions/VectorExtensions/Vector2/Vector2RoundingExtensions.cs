using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Vector2RoundingExtensions {

	public static Vector2 Round(this Vector2 vector2) => new(
		Mathf.Round(vector2.x),
		Mathf.Round(vector2.y)
	);

	public static Vector2 Ceil(this Vector2 vector2) => new(
		Mathf.Ceil(vector2.x),
		Mathf.Ceil(vector2.y)
	);

	public static Vector2 Floor(this Vector2 vector2) => new(
		Mathf.Floor(vector2.x),
		Mathf.Floor(vector2.y)
	);

	public static Vector2Int RoundToInt(this Vector2 vector2) => new(
		Mathf.RoundToInt(vector2.x),
		Mathf.RoundToInt(vector2.y)
	);

	public static Vector2Int CeilToInt(this Vector2 vector2) => new(
		Mathf.CeilToInt(vector2.x),
		Mathf.CeilToInt(vector2.y)
	);

	public static Vector2Int FloorToInt(this Vector2 vector2) => new(
		Mathf.FloorToInt(vector2.x),
		Mathf.FloorToInt(vector2.y)
	);

}