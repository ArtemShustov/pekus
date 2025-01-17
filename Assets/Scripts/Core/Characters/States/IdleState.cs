using UnityEngine;

namespace Core.Characters.States {
	public class IdleState: CharacterState {
		[Header("Settings")]
		[SerializeField] private bool _isDefault = false;
		[Header("Transitions")]
		[SerializeField] private CharacterState _moveState;

		public override void Init(CharacterStateMachine character) {
			base.Init(character);
			if (_isDefault) {
				Character.ChangeState(this);
			}
		}
		public override void CheckTransitions() {
			if (Character.Input.Movement.sqrMagnitude > 0) {
				Character.ChangeState(_moveState);
			}
		}
	}
}