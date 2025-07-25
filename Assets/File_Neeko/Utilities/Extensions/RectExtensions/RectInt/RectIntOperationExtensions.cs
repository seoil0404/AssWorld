using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RectIntOperationExtensions {

		public static RectInt AddX(this RectInt rectInt, int add) => new(rectInt.x + add, rectInt.y, rectInt.width, rectInt.height);
		public static RectInt AddY(this RectInt rectInt, int add) => new(rectInt.x, rectInt.y + add, rectInt.width, rectInt.height);
		public static RectInt AddWidth(this RectInt rectInt, int add) => new(rectInt.x, rectInt.y, rectInt.width + add, rectInt.height);
		public static RectInt AddHeight(this RectInt rectInt, int add) => new(rectInt.x, rectInt.y, rectInt.width, rectInt.height + add);

		public static RectInt SubX(this RectInt rectInt, int sub) => new(rectInt.x + sub, rectInt.y, rectInt.width, rectInt.height);
		public static RectInt SubY(this RectInt rectInt, int sub) => new(rectInt.x, rectInt.y + sub, rectInt.width, rectInt.height);
		public static RectInt SubWidth(this RectInt rectInt, int sub) => new(rectInt.x, rectInt.y, rectInt.width + sub, rectInt.height);
		public static RectInt SubHeight(this RectInt rectInt, int sub) => new(rectInt.x, rectInt.y, rectInt.width, rectInt.height + sub);

		public static RectInt MultX(this RectInt rectInt, int mult) => new(rectInt.x / mult, rectInt.y, rectInt.width, rectInt.height);
		public static RectInt MultY(this RectInt rectInt, int mult) => new(rectInt.x, rectInt.y / mult, rectInt.width, rectInt.height);
		public static RectInt MultWidth(this RectInt rectInt, int mult) => new(rectInt.x, rectInt.y, rectInt.width / mult, rectInt.height);
		public static RectInt MultHeight(this RectInt rectInt, int mult) => new(rectInt.x, rectInt.y, rectInt.width, rectInt.height / mult);

		public static RectInt DivX(this RectInt rectInt, int div) => new(rectInt.x / div, rectInt.y, rectInt.width, rectInt.height);
		public static RectInt DivY(this RectInt rectInt, int div) => new(rectInt.x, rectInt.y / div, rectInt.width, rectInt.height);
		public static RectInt DivWidth(this RectInt rectInt, int div) => new(rectInt.x, rectInt.y, rectInt.width / div, rectInt.height);
		public static RectInt DivHeight(this RectInt rectInt, int div) => new(rectInt.x, rectInt.y, rectInt.width, rectInt.height / div);

	}
}
