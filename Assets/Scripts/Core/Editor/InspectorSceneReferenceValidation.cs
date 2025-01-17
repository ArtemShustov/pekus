using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Core.Editor {
	[CustomPropertyDrawer(typeof(SceneReferenceAttribute))]
	public class InspectorSceneReferenceValidation: PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (property.propertyType != SerializedPropertyType.String) {
				EditorGUI.LabelField(position, label.text, "Use [SceneValidation] only on strings.");
				return;
			}

			var fieldPosition = position;
			fieldPosition.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(fieldPosition, property, label);
			
			if (!IsValidSceneName(property.stringValue)) {
				var warningPosition = new Rect(
					position.x, position.y + EditorGUIUtility.singleLineHeight * 1, 
					position.width, EditorGUIUtility.singleLineHeight * 2
				);
				EditorGUI.HelpBox(warningPosition, "Scene is not in the build.", MessageType.Warning);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			if (property.propertyType == SerializedPropertyType.String && !IsValidSceneName(property.stringValue)) {
				return EditorGUIUtility.singleLineHeight * 3;
			}
			return EditorGUIUtility.singleLineHeight;
		}

		private bool IsValidSceneName(string sceneName) {
			return !string.IsNullOrEmpty(sceneName) && Array.Exists(
				EditorBuildSettings.scenes, 
				scene => Path.GetFileNameWithoutExtension(scene.path) == sceneName
			);
		}
	}
}