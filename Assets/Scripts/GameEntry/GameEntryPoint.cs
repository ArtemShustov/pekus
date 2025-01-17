using UnityEngine;

namespace Game.GameEntry {
	public static class GameEntryPoint {
		private static GameContext _context;
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void Load() {
			_context = new GameContext();

			var bootstrap = Object.FindFirstObjectByType<Bootstrap>();
			if (bootstrap == null) {
				Debug.LogWarning("Bootstrap not found. Check scene order.");
				return;
			}
			bootstrap.Run(_context).Forget();
		}
	}
}