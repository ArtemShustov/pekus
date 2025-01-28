using Core.Debugging;
using UnityEditor;
using UnityEngine;

namespace Core.Debugging.Editor {
	[CustomPropertyDrawer(typeof(LoggerReference))]
	public class LoggerReferenceDrawer: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			EditorGUI.PropertyField(position, property.FindPropertyRelative("_logger"), GUIContent.none);
			EditorGUI.EndProperty();
		}
	}
}