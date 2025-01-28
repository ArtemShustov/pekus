using UnityEngine;

namespace Game.Gameplay {
	public class LocationSpawnPoint: MonoBehaviour {
		[SerializeField] private string _id;

		public string Id => _id;
	}
}