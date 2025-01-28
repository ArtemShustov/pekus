using Core.Characters;
using Core.Debugging;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay {
	public class World: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private LocationController _locationController;
		[SerializeField] private LoggerReference _logger;
		
		public void SetContainer(DIContainer container) {
			_locationController.SetContainer(container);
			_locationController.Inject(container);
		}
		public void SetPlayer(PlayerCharacter player) {
			_locationController.SetPlayer(player);
		}

		public async UniTask ChangeLocationAsync(string location) {
			await _locationController.ChangeLocationImmediateAsync(location);
		}
		public async UniTask ChangeLocationAsync(string location, string spawnPoint) {
			await _locationController.ChangeLocationAsync(location, spawnPoint);
		}
	}
}