using UnityEngine;

namespace Core.Characters {
	public class CharacterStateMachine: MonoBehaviour {
		[SerializeField] private bool _applyRootMotion = true;
		[Header("Components")]
		[SerializeField] private CharacterController _controller;
		[SerializeField] private CharacterModel _model;
		private readonly InputContainer _input = new InputContainer();
		private ICharacterState _currentState;
		
		public CharacterController Controller => _controller;
		public CharacterModel Model => _model;
		public ICharacterInput Input => _input;
		public ICharacterState State => _currentState;

		private void Start() {
			// Init states
			foreach (var state in GetComponentsInChildren<ICharacterState>()) {
				state.Init(this);
			}
			// Get input
			if (TryGetComponent<ICharacterInput>(out var input)) {
				_input.Change(input);
			}
		}
		private void Update() {
			if (_currentState != null) {
				_currentState.CheckTransitions();
				_currentState.OnUpdate();
			}
		}
		
		public void ChangeState(ICharacterState state) {
			_currentState?.OnExit();
			_currentState = state;
			_currentState?.OnEnter();
		}
		public void SetInput(ICharacterInput input) {
			_input.Change(input);
		}
		public void SetApplyRootMotion(bool value) {
			_applyRootMotion = value;
		}

		private void OnAnimatorMoved(Vector3 deltaPosition) {
			if (_applyRootMotion) {
				_controller.Move(deltaPosition + Physics.gravity * Time.deltaTime);
			}
		}
		private void OnEnable() {
			_model.AnimatorMove += OnAnimatorMoved;
		}
		private void OnDisable() {
			_model.AnimatorMove -= OnAnimatorMoved;
		}
	}
}