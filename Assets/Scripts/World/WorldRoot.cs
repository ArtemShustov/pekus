using Core.Debugging;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using Game.Player;
using UnityEngine;

namespace Game.World {
	public class WorldRoot: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private LocationController _locationController;
		[SerializeField] private LoggerReference _logger;
		[SerializeField] private WorldClock _clock;
		
		public WorldClock Clock => _clock;
		
		public void SetContainer(DIContainer container) {
			_locationController.SetContainer(container);
			_locationController.Inject(container);
		}

		public async UniTask ChangeLocationAsync(string location) {
			await _locationController.ChangeLocationImmediateAsync(location);
		}
		public async UniTask ChangeLocationAsync(PlayerCharacter player, string location, string spawnPoint) {
			await _locationController.ChangeLocationAsync(player, location, spawnPoint);
		}
	}
}