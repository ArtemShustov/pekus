using Core.Characters;
using Core.Debugging;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
	public class LocationController: MonoBehaviour {
		[SerializeField] private LoggerReference _logger;
		[Inject] private LoadingScreen _loadingScreen;
		[Inject] private GameCamera _camera;
		private PlayerCharacter _player;
		private DIContainer _container;
		private Scene _currentScene;

		public void SetContainer(DIContainer container) {
			_container = container;
		}
		public void SetPlayer(PlayerCharacter player) {
			_player = player;
		}
		
		public async UniTask<LocationRoot> ChangeLocationImmediateAsync(string sceneName) {
			if (_currentScene.isLoaded) {
				await SceneManager.UnloadSceneAsync(_currentScene);
			}
			await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			_currentScene = SceneManager.GetSceneByName(sceneName);

			var locationRoot = FindFirstObjectByType<LocationRoot>();
			if (!locationRoot) {
				_logger.LogError("Could not find location root");
			}
			_currentScene.Inject(_container);
			await locationRoot.Enter();
			
			_logger.Log($"Location '{sceneName}' loaded");

			return locationRoot;
		}
		public async UniTask ChangeLocationAsync(string sceneName, string spawnPoint) {
			_player.DisableInput();
			_camera.DisableInput();
			await _loadingScreen.ShowAsync();
			
			var locationRoot = await ChangeLocationImmediateAsync(sceneName);
			
			if (!locationRoot.TryGetSpawnPoint(spawnPoint, out var spawn)) {
				_logger.LogError($"Could not find spawn point '{spawnPoint}' on '{sceneName}'");
				return;
			} 
			var lastPosition = _player.transform.position;
			_player.transform.SetPositionAndRotation(spawn.transform.position, spawn.transform.rotation);
			_camera.OnTargetObjectWarped(_player.transform.position - lastPosition);
			Physics.SyncTransforms();
			
			await _loadingScreen.HideAsync();
			_player.EnableInput();
			_camera.EnableInput();
		}
	}
}