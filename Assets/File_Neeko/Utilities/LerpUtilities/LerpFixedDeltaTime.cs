using UnityEngine;

namespace Neeko {

	public static class LerpFixedDeltaTime {

		private static float Factor(float smoothness) =>
			1f - Mathf.Exp(-1f / smoothness * Time.fixedDeltaTime);

		public static float Lerp(float a, float b, float smoothness) =>
			Mathf.Lerp(a, b, Factor(smoothness));

		public static Vector2 Lerp(Vector2 a, Vector2 b, float smoothness) =>
			Vector2.Lerp(a, b, Factor(smoothness));

		public static Vector3 Lerp(Vector3 a, Vector3 b, float smoothness) =>
			Vector3.Lerp(a, b, Factor(smoothness));

		public static Vector4 Lerp(Vector4 a, Vector4 b, float smoothness) =>
			Vector4.Lerp(a, b, Factor(smoothness));
		
		public static Color Lerp(Color a, Color b, float smoothness) =>
			Color.Lerp(a, b, Factor(smoothness));

		public static Quaternion Lerp(Quaternion a, Quaternion b, float smoothness) =>
			Quaternion.Lerp(a, b, smoothness);

		public static float LerpAngle(float a, float b, float smoothness) =>
			Mathf.LerpAngle(a, b, smoothness);

	}

}
