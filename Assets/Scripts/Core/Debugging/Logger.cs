using UnityEngine;

namespace Core.Debugging {
	[CreateAssetMenu(menuName = "Debug/Logger")]
	public class Logger: ScriptableObject {
		[SerializeField] private bool _enabled = true;
		
		public virtual void Log(string message) {
			if (!_enabled) return;
			Debug.Log(message);
		}
		public virtual void LogError(string message) {
			if (!_enabled) return;
			Debug.LogError(message);
		}
	}
}