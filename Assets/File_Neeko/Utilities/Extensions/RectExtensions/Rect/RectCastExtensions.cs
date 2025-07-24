using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class RectCastExtensions {

	public static RectInt ToRectInt(this Rect rect) => new(
		rect.position.ToVector2Int(),
		rect.size.ToVector2Int()
	);

}