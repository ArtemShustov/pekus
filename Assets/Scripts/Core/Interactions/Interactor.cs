using Core.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Interactions {
	public class Interactor: MonoBehaviour {
		[SerializeField] private Character _character;
		[SerializeField] private InputAction _button = new InputAction("Button", InputActionType.Button, "<Gamepad>/buttonWest");
		private Interaction _current;

		private void OnButton(InputAction.CallbackContext obj) {
			_current?.Run(_character);
		}
		private void OnEnable() {
			_button.Enable();
			_button.performed += OnButton;
		}
		private void OnDisable() {
			_button.performed -= OnButton;
			_button.Disable();
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
		private void OnDestroy() {
			_current?.SetSelected(false);
		}
	}
}