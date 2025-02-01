using Core;
using Core.Characters;
using Core.Debugging;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameEntry {
	public class Bootstrap: MonoBehaviour {
		[SerializeField] private LoadingScreen _loadingScreen;
		[SerializeField] private LoggerReference _logger;
		
		[SceneReference] private const string NextSceneName = "World";
		private GameContext _context; 
		
		public async UniTaskVoid Run(GameContext context) {
			_context = context;
			
			_logger.Log("Bootstrap started");
			DontDestroyOnLoad(_loadingScreen.gameObject);
			context.Container.RegisterInstance(_loadingScreen);
			
			_logger.Log("Loading world");
			await SceneManager.LoadSceneAsync(NextSceneName);
			
			_logger.Log("World init");
			await Awaitable.NextFrameAsync();
			var world = await InitWorldScene();
			await Awaitable.NextFrameAsync();
			await _loadingScreen.HideAsync();
			world.PostInit();
			
			Debug.Log("Bootstrap finished.");
		}
		private async UniTask<WorldEntryPoint> InitWorldScene() {
			var worldEntryPoint = FindFirstObjectByType<WorldEntryPoint>();
			if (worldEntryPoint == null) {
				Debug.LogError("World entry point not found."); 
				return null;
			}
			
			await worldEntryPoint.Run(_context);

			return worldEntryPoint;
		}
	}
}