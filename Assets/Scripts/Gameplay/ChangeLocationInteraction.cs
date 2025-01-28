using Core;
using Core.Characters;
using Core.DependencyInjection;
using Core.Interactions;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay {
	public class ChangeLocationInteraction: Interaction {
		[SceneReference]
		[SerializeField] private string _locationName;
		[SerializeField] private string _spawnPointId;
		[Inject] private World _root;
		
		public override void Run(Character character) {
			base.Run(character);
			ChangeLocation(character).Forget();
		}

		private async UniTaskVoid ChangeLocation(Character character) {
			await _root.ChangeLocationAsync(_locationName, _spawnPointId);
		}
	}
}