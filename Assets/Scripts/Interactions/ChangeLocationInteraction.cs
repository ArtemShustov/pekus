using Core;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using Game.Players;
using Game.World;
using UnityEngine;

namespace Game.Interactions {
	public class ChangeLocationInteraction: Interaction {
		[SceneReference]
		[SerializeField] private string _locationName;
		[SerializeField] private string _spawnPointId;
		[Inject] private WorldRoot _root;
		
		public override void Run(Player player) {
			base.Run(player);
			ChangeLocation(player).Forget();
		}

		private async UniTaskVoid ChangeLocation(Player player) {
			await _root.ChangeLocationAsync(player, _locationName, _spawnPointId);
		}
	}
}