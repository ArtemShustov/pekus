using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Core.Editor {
	[InitializeOnLoad]
	public static class ConstSceneReferenceValidation {
		private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
		
		static ConstSceneReferenceValidation() {
			AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
		}
		
		private static void OnAfterAssemblyReload() {
			var types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes());
			foreach (var type in types) {
				var fields = type.GetFields(Flags)
					.Where(field => field.GetCustomAttribute<SceneReferenceAttribute>() != null)
					.Where(field => field.IsLiteral && field.FieldType == typeof(string));
				foreach (var field in fields) {
					ValidateField(type, field);
				}
			}
		}

		private static void ValidateField(Type type, FieldInfo field) {
			var sceneName = (string)field.GetRawConstantValue();
			if (string.IsNullOrEmpty(sceneName) || IsSceneInBuild(sceneName)) {
				return;
			}
			Debug.LogWarning($"Type {type.FullName} references scene {sceneName}, which is not in the build.");
		}
		private static bool IsSceneInBuild(string sceneName) {
			if (string.IsNullOrEmpty(sceneName)) {
				return false;
			}
			return Array.Exists(
				EditorBuildSettings.scenes, 
				scene => Path.GetFileNameWithoutExtension(scene.path) == sceneName
			);
		}
	}
}