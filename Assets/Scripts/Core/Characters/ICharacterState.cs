namespace Core.Characters {
	public interface ICharacterState {
		void Init(CharacterStateMachine character);
		
		void CheckTransitions();
		void OnUpdate();

		void OnEnter();
		void OnExit();
	}
}