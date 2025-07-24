using UnityEngine;

namespace Neeko {

	public static class LerpFixedDeltaTime {

		private static float Factor(float speed) =>
			1f - Mathf.Pow(1f - speed * 3f, Time.fixedDeltaTime * 50f);

		public static float Lerp(float a, float b, float speed) =>
			Mathf.Lerp(a, b, Factor(speed));

		public static Vector2 Lerp(Vector2 a, Vector2 b, float speed) =>
			Vector2.Lerp(a, b, Factor(speed));

		public static Vector3 Lerp(Vector3 a, Vector3 b, float speed) =>
			Vector3.Lerp(a, b, Factor(speed));

		public static Vector4 Lerp(Vector4 a, Vector4 b, float speed) =>
			Vector4.Lerp(a, b, Factor(speed));
		
		public static Color Lerp(Color a, Color b, float speed) =>
			Color.Lerp(a, b, Factor(speed));

		public static Quaternion Lerp(Quaternion a, Quaternion b, float speed) =>
			Quaternion.Lerp(a, b, speed);

		public static float LerpAngle(float a, float b, float speed) =>
			Mathf.LerpAngle(a, b, speed);

	}

}
