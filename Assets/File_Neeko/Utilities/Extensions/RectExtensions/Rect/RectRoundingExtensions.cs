using System.ComponentModel;
using UnityEngine;

namespace Neeko {

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RectRoundingExtensions {

		public static Rect Round(this Rect rect) => new(
			rect.position.Round(),
			rect.size.Round()
		);	

		public static Rect Ceil(this Rect rect) => new(
			rect.position.Ceil(),
			rect.size.Ceil()
		);

		public static Rect Floor(this Rect rect) => new(
			rect.position.Floor(),
			rect.size.Floor()
		);

		public static RectInt RoundToInt(this Rect rect) => new(
			rect.position.RoundToInt(),
			rect.size.RoundToInt()
		);	

		public static RectInt CeilToInt(this Rect rect) => new(
			rect.position.CeilToInt(),
			rect.size.CeilToInt()
		);

		public static RectInt FloorToInt(this Rect rect) => new(
			rect.position.FloorToInt(),
			rect.size.FloorToInt()
		);

	}
}
