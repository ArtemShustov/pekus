using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Editor {
	[CustomEditor(typeof(LocationSpawnPoint))]
	public class LocationSpawnPointEditor: UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			
			var spawnPoint = (LocationSpawnPoint)target;
			
			var roots = FindObjectsByType<LocationRoot>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
			if (roots.Length == 0) {
				EditorGUILayout.HelpBox("No LocationRoot found.", MessageType.Error);
				return;
			} else if (roots.Length > 1) {
				EditorGUILayout.HelpBox("More than one LocationRoot found.", MessageType.Error);
				return;
			}
			var root = roots[0];
			if (root.SpawnPoints.Contains(spawnPoint)) {
				return;
			}
			if (GUILayout.Button($"Add to LocationRoot '{root.name}'")) {
				root.AddSpawnPoint(spawnPoint);
				return;
			}
		}
	}
}