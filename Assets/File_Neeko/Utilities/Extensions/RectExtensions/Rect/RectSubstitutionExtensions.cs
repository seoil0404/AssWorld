using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class RectSubstitutionExtensions {

	public static Rect WithX(this Rect rect, float x) => new(x, rect.y, rect.width, rect.height);
	public static Rect WithY(this Rect rect, float y) => new(rect.x, y, rect.width, rect.height);
	public static Rect WithWidth(this Rect rect, float width) => new(rect.x, rect.y, width, rect.height);
	public static Rect WithHeight(this Rect rect, float height) => new(rect.x, rect.y, rect.width, height);

}