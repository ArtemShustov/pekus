using Core;
using Core.DependencyInjection;
using Cysharp.Threading.Tasks;
using Game.Player;
using Game.World;
using UnityEngine;

namespace Game.Interactions {
	public class ChangeLocationInteraction: Interaction {
		[SceneReference]
		[SerializeField] private string _locationName;
		[SerializeField] private string _spawnPointId;
		[Inject] private WorldRoot _root;
		
		public override void Run(PlayerCharacter player) {
			base.Run(player);
			ChangeLocation(player).Forget();
		}

		private async UniTaskVoid ChangeLocation(PlayerCharacter player) {
			await _root.ChangeLocationAsync(player, _locationName, _spawnPointId);
		}
	}
}