using Game.Gameplay;
using UnityEngine;

namespace Game.Players {
	public class Player {
		private GameCamera _camera;
		private PlayerCharacter _character;
		
		public PlayerCharacter Character => _character;

		public void Teleport(Vector3 position, Quaternion rotation) {
			var lastPosition = _character.transform.position;
			_character.transform.SetPositionAndRotation(position, rotation);
			_camera?.OnTargetObjectWarped(_character.transform.position - lastPosition);
			Physics.SyncTransforms();
		}
		
		public void SetCharacter(PlayerCharacter character) {
			_character = character;
			_character.SetPlayer(this);
			_camera.SetTarget(character.transform);
		}
		public void SetCamera(GameCamera camera) {
			_camera = camera;
			if (_character != null) {
				_camera.SetTarget(_character.transform);
			}
		}

		public void EnableInput() {
			_character?.EnableInput();
			_camera?.EnableInput();
		}
		public void DisableInput() {
			_character?.DisableInput();
			_camera?.DisableInput();
		}
	}
}