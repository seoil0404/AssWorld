using UnityEditor;
using UnityEngine;

namespace Neeko {

	[CustomPropertyDrawer(typeof(PerBase))]
	public class PerDrawer : PropertyDrawer {

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.BeginChangeCheck();

			var rateProperty = property.FindPropertyRelative("_rate");
			if (rateProperty is null) {
				EditorGUI.LabelField(position, "Invalid Per<TUnit>: _rate not found");
				EditorGUI.EndProperty();
				return;
			}

			var newRate = EditorGUI.FloatField(position, label, rateProperty.floatValue);
			if (EditorGUI.EndChangeCheck()) {
				rateProperty.floatValue = newRate;
			}

			EditorGUI.EndProperty();

		}

	}

}
