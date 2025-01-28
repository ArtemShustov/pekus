using System.Collections.Generic;
using System.Linq;
using Core.Characters;
using Core.Debugging;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay {
	public class LocationRoot: MonoBehaviour {
		[SerializeField] private List<LocationSpawnPoint> _spawnPoints;
		[SerializeField] private LoggerReference _logger;
		
		protected LoggerReference Logger => _logger;
		public IReadOnlyList<LocationSpawnPoint> SpawnPoints => _spawnPoints;

		public virtual UniTask Enter() {
			return UniTask.CompletedTask;
		}

		public void AddSpawnPoint(LocationSpawnPoint spawnPoint) {
			if (!_spawnPoints.Contains(spawnPoint)) {
				_spawnPoints.Add(spawnPoint);
			}
		}
		public bool TryGetSpawnPoint(string id, out LocationSpawnPoint location) {
			location = _spawnPoints.FirstOrDefault(point => point.Id == id);
			return location != null;
		}
	}
}