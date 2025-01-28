using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Editor {
	[CustomEditor(typeof(LocationRoot))]
	public class LocationRootEditor: UnityEditor.Editor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			
			var root = (LocationRoot)target;
			
			
			if (GUILayout.Button("Add spawn points from scene")) {
				var spawnPoints = root.SpawnPoints;
				foreach (var spawnPoint in FindObjectsByType<LocationSpawnPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)) {
					if (!spawnPoints.Contains(spawnPoint)) {
						root.AddSpawnPoint(spawnPoint);
					}
				}
			}
		}
	}
}