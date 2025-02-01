using Core.Characters;

namespace Game.Gameplay {
	public class Player {
		private PlayerCharacter _character;
		private World _world;
		
		public Player(World world) {
			_world = world;
		}

		public void SetCharacter(PlayerCharacter character) {
			_character = character;
		}
		
		public void EnableInput() => _character?.EnableInput();
		public void DisableInput() => _character?.DisableInput();
	}
}