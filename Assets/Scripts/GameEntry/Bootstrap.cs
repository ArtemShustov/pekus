using Core;
using Core.Characters;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameEntry {
	public class Bootstrap: MonoBehaviour {
		[SerializeField] private PlayerCharacter _playerPrefab;
		[SerializeField] private LoadingScreen _loadingScreen;
		
		[SceneReference] private const string NextSceneName = "TestWorld";
		private GameContext _context; 
		
		public async UniTaskVoid Run(GameContext context) {
			_context = context;
			
			Debug.Log("Bootstrap started.");
			DontDestroyOnLoad(_loadingScreen.gameObject);
			context.Container.RegisterInstance(_loadingScreen);
			
			Debug.Log("Loading next scene.");
			await SceneManager.LoadSceneAsync(NextSceneName);
			var world = InitWorldScene();
			await Awaitable.NextFrameAsync();
			await _loadingScreen.HideAsync();
			world.PostInit();
			
			Debug.Log("Bootstrap finished.");
		}
		private WorldEntryPoint InitWorldScene() {
			var worldEntryPoint = FindFirstObjectByType<WorldEntryPoint>();
			if (worldEntryPoint == null) {
				Debug.LogError("World entry point not found.");
				return null;
			}
			
			var player = Instantiate(_playerPrefab);
			worldEntryPoint.Run(_context, player);

			return worldEntryPoint;
		}
	}
}