using UnityEngine;

namespace Core.Characters {
	public class Character: MonoBehaviour {
		[SerializeField] private CharacterStateMachine _stateMachine;

		public void SetInput(ICharacterInput input) {
			_stateMachine.SetInput(input);
		}
	}
}