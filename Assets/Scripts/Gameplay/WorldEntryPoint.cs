using Core;
using Core.Characters;
using Core.DependencyInjection;
using Core.UI.Popups;
using Cysharp.Threading.Tasks;
using Game.GameEntry;
using UnityEngine;

namespace Game.Gameplay {
	public class WorldEntryPoint: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private PopupCanvas _popupCanvas;
		[SerializeField] private GameCamera _camera;
		[SerializeField] private World _root;
		[Header("Settings")]
		[SceneReference]
		[SerializeField] private string _locationName = "TestLocation";
		[SerializeField] private PlayerCharacter _playerPrefab;
		
		private PlayerCharacter _player;
		
		public async UniTask Run(GameContext gameContext) {
			// DI
			var container = new DIContainer(gameContext.Container);
			container.RegisterInstance(_popupCanvas);
			container.RegisterInstance(_root);
			container.RegisterInstance(_camera);
			_root.SetContainer(container);
			// Init
			_player = Instantiate(_playerPrefab);
			_player.Inject(container);
			_player.DisableInput();
			_camera.SetTarget(_player.transform);
			_root.SetPlayer(_player);
			// Load location
			await _root.ChangeLocationAsync(_locationName);
		}
		public void PostInit() {
			_player.EnableInput();
			_camera.EnableInput();
		}
	}
}