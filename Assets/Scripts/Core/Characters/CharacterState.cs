using UnityEngine;

namespace Core.Characters {
	public abstract class CharacterState: MonoBehaviour, ICharacterState {
		protected CharacterStateMachine Character { get; private set; }

		public virtual void Init(CharacterStateMachine character) {
			Character = character;
		}

		public virtual void CheckTransitions() { }
		public virtual void OnUpdate() { }
		public virtual void OnEnter() { }
		public virtual void OnExit() { }
	}
}