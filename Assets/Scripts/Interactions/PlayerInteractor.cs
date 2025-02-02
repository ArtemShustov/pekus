using Game.Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Interactions {
	public class PlayerInteractor: MonoBehaviour {
		[SerializeField] private PlayerCharacterInput _input;
		private Interaction _selected;
		private Player _player;

		public void SetPlayer(Player player) {
			_player = player;
		}
		
		private void OnUse(InputActionPhase phase) {
			if (phase == InputActionPhase.Performed && _selected) {
				_selected?.Run(_player);
			}
		}
		
		private void OnTriggerEnter(Collider other) {
			_selected?.SetSelected(false);
			if (other.TryGetComponent<Interaction>(out var interaction)) {
				_selected = interaction;
				_selected.SetSelected(true);
			} else {
				_selected = null;
			}
		}
		private void OnTriggerExit(Collider other) {
			if (other.TryGetComponent<Interaction>(out var interaction) && interaction == _selected) {
				_selected?.SetSelected(false);
				_selected = null;
			}
		}
		
		private void OnEnable() {
			_input.Use += OnUse;
		}
		private void OnDisable() {
			_input.Use -= OnUse;
		}
		private void OnDestroy() {
			_selected?.SetSelected(false);
		}
	}
}