using Core;
using Core.DependencyInjection;
using Core.UI.Popups;
using Cysharp.Threading.Tasks;
using Game.GameEntry;
using Game.Players;
using Game.World;
using UnityEngine;

namespace Game.Gameplay {
	public class WorldEntryPoint: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private PopupCanvas _popupCanvas;
		[SerializeField] private GameCamera _camera;
		[SerializeField] private WorldRoot _root;
		[Header("Settings")]
		[SceneReference]
		[SerializeField] private string _locationName = "TestLocation";
		[SerializeField] private PlayerCharacter _playerPrefab;
		
		private Player _player;
		
		public async UniTask Run(GameContext gameContext) {
			// DI
			var container = new DIContainer(gameContext.Container);
			container.RegisterInstance(_popupCanvas);
			container.RegisterInstance(_root);
			container.RegisterInstance(_camera);
			_root.SetContainer(container);
			// Init
			_player = CreatePlayer(container, _camera);
			// Load location
			await _root.ChangeLocationAsync(_locationName);
		}
		public void PostInit() {
			_player.EnableInput();
		}

		private Player CreatePlayer(DIContainer container, GameCamera gameCamera) {
			var player = new Player();
			player.SetCamera(gameCamera);
			
			var character = Instantiate(_playerPrefab);
			character.Inject(container);
			player.SetCharacter(character);
			
			return player;
		}
	}
}