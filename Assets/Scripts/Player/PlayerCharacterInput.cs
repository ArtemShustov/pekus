using System;
using Core.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player {
	public class PlayerCharacterInput: MonoBehaviour, ICharacterInput {
		[SerializeField] private Camera _camera;
		private PlayerInputActions _input;
		
		public Vector2 Movement { get; private set; }
		public event Action<InputActionPhase> Use;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
			_input = new PlayerInputActions();
		}
		private void Update() {
			if (_input.Character.enabled) {
				Movement = GetRelatedMove();
			}
		}

		public void EnableInput() {
			_input.Character.Enable();
		}
		public void DisableInput() {
			Movement = Vector2.zero;
			_input.Character.Disable();
		}
		
		private Vector2 GetRelatedMove() {
			var input = _input.Character.Move.ReadValue<Vector2>();

			var directionAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
			if (directionAngle < 0) {
				directionAngle += 360;
			}
			directionAngle += _camera.transform.eulerAngles.y;
			if (directionAngle > 360) {
				directionAngle -= 360;
			}
			var forward = Quaternion.Euler(0, directionAngle, 0) * Vector3.forward;
			var result = forward * Mathf.Clamp01(input.magnitude);
			return new Vector2(result.x, result.z);
		}

		private void OnUsePerformed(InputAction.CallbackContext obj) {
			Use?.Invoke(InputActionPhase.Performed);
		}
		private void OnEnable() {
			_input.Character.Use.performed += OnUsePerformed;
		}
		private void OnDisable() {
			Movement = Vector2.zero;
			_input.Character.Use.performed -= OnUsePerformed;
		}
	}
}