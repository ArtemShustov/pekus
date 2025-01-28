using Core.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Interactions {
	public class PlayerInteractor: MonoBehaviour {
		[SerializeField] private Character _character;
		[SerializeField] private PlayerCharacterInput _input;
		private Interaction _current;
		
		private void OnUse(InputActionPhase phase) {
			if (phase == InputActionPhase.Performed && _current) {
				_current?.Run(_character);
			}
		}
		
		private void OnTriggerEnter(Collider other) {
			_current?.SetSelected(false);
			if (other.TryGetComponent<Interaction>(out var interaction)) {
				_current = interaction;
				_current.SetSelected(true);
			} else {
				_current = null;
			}
		}
		private void OnTriggerExit(Collider other) {
			if (other.TryGetComponent<Interaction>(out var interaction) && interaction == _current) {
				_current?.SetSelected(false);
				_current = null;
			}
		}
		
		private void OnEnable() {
			_input.Use += OnUse;
		}
		private void OnDisable() {
			_input.Use -= OnUse;
		}
		private void OnDestroy() {
			_current?.SetSelected(false);
		}
	}
}