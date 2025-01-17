using UnityEngine;

namespace Core.Characters {
	public class PlayerCharacter: Character {
		[SerializeField] private PlayerCharacterInput _input;
		
		public void EnableInput() => _input.enabled = true;
		public void DisableInput() => _input.enabled = false;
	}
}