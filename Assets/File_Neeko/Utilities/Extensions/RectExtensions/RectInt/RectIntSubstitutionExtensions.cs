using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RectIntSubstitutionExtensions {

		public static RectInt WithX(this RectInt rectInt, int x) => new(x, rectInt.y, rectInt.width, rectInt.height);
		public static RectInt WithY(this RectInt rectInt, int y) => new(rectInt.x, y, rectInt.width, rectInt.height);
		public static RectInt WithWidth(this RectInt rectInt, int width) => new(rectInt.x, rectInt.y, width, rectInt.height);
		public static RectInt WithHeight(this RectInt rectInt, int height) => new(rectInt.x, rectInt.y, rectInt.width, height);

	}
}
