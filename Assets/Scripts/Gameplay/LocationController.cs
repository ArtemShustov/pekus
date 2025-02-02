using Game.Players;
using Core.Debugging;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
	public class LocationController: MonoBehaviour {
		[SerializeField] private LoggerReference _logger;
		[Inject] private LoadingScreen _loadingScreen;
		private DIContainer _container;
		private Scene _currentScene;

		public void SetContainer(DIContainer container) {
			_container = container;
		}
		
		public async UniTask<LocationRoot> ChangeLocationImmediateAsync(string sceneName) {
			if (_currentScene.isLoaded) {
				await SceneManager.UnloadSceneAsync(_currentScene);
			}
			
			_logger.Log("Scene loading");
			await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			_currentScene = SceneManager.GetSceneByName(sceneName);

			_logger.Log("Scene init");
			var locationRoot = await InitLocation();
			SceneManager.SetActiveScene(_currentScene);
			
			_logger.Log($"Location '{sceneName}' loaded");

			return locationRoot;
		}
		public async UniTask ChangeLocationAsync(Player player, string sceneName, string spawnPoint) {
			player.DisableInput();
			await _loadingScreen.ShowAsync();
			
			var locationRoot = await ChangeLocationImmediateAsync(sceneName);
			
			if (locationRoot.TryGetSpawnPoint(spawnPoint, out var spawn)) {
				player.Teleport(spawn.transform.position, spawn.transform.rotation);
			} else {
				_logger.LogError($"Could not find spawn point '{spawnPoint}' on '{sceneName}'");
			}

			await _loadingScreen.HideAsync();
			player.EnableInput();
		}
		
		private async UniTask<LocationRoot> InitLocation() {
			Profiler.BeginSample("Location initialization");
			
			var locationRoot = FindFirstObjectByType<LocationRoot>();
			if (!locationRoot) {
				_logger.LogError("Could not find location root");
			}
			_currentScene.Inject(_container);
			await locationRoot.Enter();
			
			Profiler.EndSample();
			return locationRoot;
		}
	}
}
