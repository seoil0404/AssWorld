using System.ComponentModel;
using UnityEngine;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class RectResizeCenterExtensions {

	public static Rect ResizeXCenter(this Rect rect, float sizeX) => rect.WithX(rect.center.x - sizeX / 2f).WithWidth(sizeX);
	public static Rect ResizeYCenter(this Rect rect, float sizeY) => rect.WithY(rect.center.y - sizeY / 2f).WithWidth(sizeY);
	public static Rect ResizeCenter(this Rect rect, Vector2 size) => rect.ResizeCenter(size.x, size.y);
	public static Rect ResizeCenter(this Rect rect, float sizeX, float sizeY) => rect.ResizeXCenter(sizeX).ResizeYCenter(sizeY);

	public static Rect AddSizeXCenter(this Rect rect, float add) => rect.ResizeXCenter(rect.width + add);
	public static Rect AddSizeYCenter(this Rect rect, float add) => rect.ResizeYCenter(rect.width + add);
	public static Rect AddSizeCenter(this Rect rect, Vector2 add) => rect.AddSizeCenter(add.x, add.y);
	public static Rect AddSizeCenter(this Rect rect, float addX, float addY) => rect.AddSizeXCenter(addX).AddSizeYCenter(addY);

	public static Rect SubSizeXCenter(this Rect rect, float sub) => rect.ResizeXCenter(rect.width - sub);
	public static Rect SubSizeYCenter(this Rect rect, float sub) => rect.ResizeYCenter(rect.width - sub);
	public static Rect SubSizeCenter(this Rect rect, Vector2 sub) => rect.SubSizeCenter(sub.x, sub.y);
	public static Rect SubSizeCenter(this Rect rect, float addX, float addY) => rect.SubSizeXCenter(addX).SubSizeYCenter(addY);

	public static Rect MultSizeXCenter(this Rect rect, float mult) => rect.ResizeXCenter(rect.width * mult);
	public static Rect MultSizeYCenter(this Rect rect, float mult) => rect.ResizeYCenter(rect.width * mult);
	public static Rect MultSizeCenter(this Rect rect, Vector2 mult) => rect.MultSizeCenter(mult.x, mult.y);
	public static Rect MultSizeCenter(this Rect rect, float addX, float addY) => rect.MultSizeXCenter(addX).MultSizeYCenter(addY);

	public static Rect DivSizeXCenter(this Rect rect, float div) => rect.ResizeXCenter(rect.width / div);
	public static Rect DivSizeYCenter(this Rect rect, float div) => rect.ResizeYCenter(rect.width / div);
	public static Rect DivSizeCenter(this Rect rect, Vector2 div) => rect.DivSizeCenter(div.x, div.y);
	public static Rect DivSizeCenter(this Rect rect, float addX, float addY) => rect.DivSizeXCenter(addX).DivSizeYCenter(addY);

}