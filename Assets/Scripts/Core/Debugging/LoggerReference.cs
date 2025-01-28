using System;
using UnityEngine;

namespace Core.Debugging {
	[Serializable]
	public struct LoggerReference {
		[field: SerializeField] private Logger _logger;

		public void Log(string message) {
			_logger?.Log(message);
		}
		public void LogError(string message) {
			_logger?.LogError(message);
		}
	}
}