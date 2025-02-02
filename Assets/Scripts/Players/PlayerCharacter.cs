using Core.Characters;
using Game.Interactions;
using UnityEngine;

namespace Game.Players {
	public class PlayerCharacter: Character {
		[SerializeField] private PlayerCharacterInput _input;
		[SerializeField] private PlayerInteractor _interactor;

		private Player _player;

		public void SetPlayer(Player player) {
			_player = player;
			_interactor.SetPlayer(_player);
		}
		
		public void EnableInput() {
			_input.enabled = true;
		}
		public void DisableInput() {
			_input.enabled = false;
		}
	}
}