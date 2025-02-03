using Core.Characters;
using Game.Interactions;
using UnityEngine;

namespace Game.Player {
	public class PlayerCharacter: Character {
		[SerializeField] private PlayerCharacterInput _input;
		[SerializeField] private PlayerInteractor _interactor;

		private GameCamera _camera; 
		private PlayerData _player;

		public void SetPlayer(PlayerData player) {
			_player = player;
		}
		public void SetCamera(GameCamera gameCamera) {
			_camera = gameCamera;
			_camera.SetTarget(transform);
		}
		
		public void Teleport(Vector3 position, Quaternion rotation) {
			var lastPosition = transform.position;
			transform.SetPositionAndRotation(position, rotation);
			_camera?.OnTargetObjectWarped(transform.position - lastPosition);
			Physics.SyncTransforms();
		}
		
		public void EnableInput() {
			_input.EnableInput();
			_camera.EnableInput();
		}
		public void DisableInput() {
			_input.DisableInput();
			_camera.DisableInput();
		}
	}
}