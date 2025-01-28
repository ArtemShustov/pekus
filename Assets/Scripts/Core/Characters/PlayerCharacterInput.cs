using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Characters {
	public class PlayerCharacterInput: MonoBehaviour, ICharacterInput {
		[SerializeField] private InputAction _moveAction = new InputAction("Move", InputActionType.Value, "<Gamepad>/leftStick");
		[SerializeField] private InputAction _useAction = new InputAction("Use", InputActionType.Button, "<Gamepad>/buttonWest");
		[SerializeField] private Camera _camera;
		
		public Vector2 Movement { get; private set; }
		public event Action<InputActionPhase> Use;

		private void Awake() {
			if (_camera == null) {
				_camera = Camera.main;
			}
		}
		private void Update() {
			Movement = GetRelatedMove();
		}
		
		private Vector2 GetRelatedMove() {
			var input = _moveAction.ReadValue<Vector2>();

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
			_moveAction.Enable();
			_useAction.Enable();
			_useAction.performed += OnUsePerformed;
		}
		private void OnDisable() {
			Movement = Vector2.zero;
			_moveAction.Disable();
			_useAction.Disable();
			_useAction.performed -= OnUsePerformed;
		}
	}
}