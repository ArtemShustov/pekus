using UnityEngine;

namespace Core.Characters.States {
	public class MoveState: CharacterState {
		[Header("Settings")]
		[SerializeField] private float _rotationSpeed = 3;
		[SerializeField] private float _zeroBufferTime = 0.05f;
		[Header("Components")]
		[SerializeField] private ParticleSystem _dustParticle;
		[Header("Transitions")]
		[SerializeField] private CharacterState _idleState;

		private float _zeroInputBuffer = 0;
		
		public override void CheckTransitions() {
			if (Character.Input.Movement.sqrMagnitude == 0) {
				_zeroInputBuffer -= Time.deltaTime;
			} else if (_zeroInputBuffer < _zeroBufferTime) {
				_zeroInputBuffer += Time.deltaTime;
			}
			
			if (_zeroInputBuffer <= 0) {
				Character.ChangeState(_idleState);
				_zeroInputBuffer = 0;
			}
		}
		public override void OnUpdate() {
			var movement = Character.Input.Movement;
			RotateTo(movement, Time.deltaTime);
		}
		public override void OnEnter() {
			Character.Model.SetIsMove(true);
			_dustParticle.Play();
		}
		public override void OnExit() {
			Character.Model.SetIsMove(false);
			_dustParticle.Stop();
		}

		private void RotateTo(Vector2 direction, float deltaTime) {
			if (direction.sqrMagnitude <= Mathf.Epsilon) return;
			
			var character = Character.Controller.transform;
			var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
			character.rotation = Quaternion.Slerp(
				character.rotation, 
				targetRotation, 
				deltaTime * _rotationSpeed
			);
		}
	}
}