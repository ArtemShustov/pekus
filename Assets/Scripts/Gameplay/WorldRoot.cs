using Core.Characters;
using Core.DependencyInjection;
using UnityEngine;

namespace Game.Gameplay {
	public class WorldRoot: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private GameCamera _camera;
		
		private PlayerCharacter _player;
		private DIContainer _container;

		public DIContainer Container => _container;
		
		public void SetContainer(DIContainer container) {
			_container = container;
		}
		public void SetPlayer(PlayerCharacter player) {
			_player = player;
			_camera.SetTarget(player.transform);
		}
	}
}