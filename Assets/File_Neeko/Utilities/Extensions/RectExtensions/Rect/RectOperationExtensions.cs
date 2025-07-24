using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class RectOperationExtensions {

	public static Rect AddX(this Rect rect, float add) => new(rect.x + add, rect.y, rect.width, rect.height);
	public static Rect AddY(this Rect rect, float add) => new(rect.x, rect.y + add, rect.width, rect.height);
	public static Rect AddWidth(this Rect rect, float add) => new(rect.x, rect.y, rect.width + add, rect.height);
	public static Rect AddHeight(this Rect rect, float add) => new(rect.x, rect.y, rect.width, rect.height + add);

	public static Rect SubX(this Rect rect, float sub) => new(rect.x + sub, rect.y, rect.width, rect.height);
	public static Rect SubY(this Rect rect, float sub) => new(rect.x, rect.y + sub, rect.width, rect.height);
	public static Rect SubWidth(this Rect rect, float sub) => new(rect.x, rect.y, rect.width + sub, rect.height);
	public static Rect SubHeight(this Rect rect, float sub) => new(rect.x, rect.y, rect.width, rect.height + sub);

	public static Rect MultX(this Rect rect, float mult) => new(rect.x / mult, rect.y, rect.width, rect.height);
	public static Rect MultY(this Rect rect, float mult) => new(rect.x, rect.y / mult, rect.width, rect.height);
	public static Rect MultWidth(this Rect rect, float mult) => new(rect.x, rect.y, rect.width / mult, rect.height);
	public static Rect MultHeight(this Rect rect, float mult) => new(rect.x, rect.y, rect.width, rect.height / mult);

	public static Rect DivX(this Rect rect, float div) => new(rect.x / div, rect.y, rect.width, rect.height);
	public static Rect DivY(this Rect rect, float div) => new(rect.x, rect.y / div, rect.width, rect.height);
	public static Rect DivWidth(this Rect rect, float div) => new(rect.x, rect.y, rect.width / div, rect.height);
	public static Rect DivHeight(this Rect rect, float div) => new(rect.x, rect.y, rect.width, rect.height / div);

}