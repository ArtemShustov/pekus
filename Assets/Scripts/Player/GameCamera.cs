using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player {
	public class GameCamera: MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private float _minZoom = 5f;
		[SerializeField] private float _maxZoom = 10f;
		[SerializeField] private float _zoomSpeed = 2f;
		[Header("Components")]
		[SerializeField] private Camera _camera;
		[SerializeField] private CinemachineCamera _cinemachine;
		[SerializeField] private CinemachineFollow _cinemachineFollow;
		
		private readonly Vector3[] _positions = {
			new Vector3(0, 1, -1), // Forward
			new Vector3(1, 1, 0), // Right
			new Vector3(0, 1, 1), // Back
			new Vector3(-1, 1, 0) // Left
		};
		private PlayerInputActions _input;
		private int _currentMode;
		private float _zoomLevel = 1f;

		private void Awake() {
			_input = new PlayerInputActions();
		}
		private void Update() {
			UpdateZoom();
		}
		
		public void SetTarget(Transform target) {
			_cinemachine.Follow = target;
			_cinemachine.LookAt = target;
			_zoomLevel = _maxZoom;
			UpdateFollow();
		}
		public void OnTargetObjectWarped(Vector3 delta) {
			_cinemachine.OnTargetObjectWarped(_cinemachine.Target.TrackingTarget, delta);
		}
		public void EnableInput() {
			_input.Camera.Enable();
		}
		public void DisableInput() {
			_input.Camera.Disable();
		}

		private void RotateRight(InputAction.CallbackContext obj) {
			_currentMode = (_currentMode + 1) % _positions.Length;
			UpdateFollow();
		}
		private void RotateLeft(InputAction.CallbackContext obj) {
			_currentMode = (_currentMode - 1 + _positions.Length) % _positions.Length;
			UpdateFollow();
		}
		private void UpdateZoom() {
			float zoomInput = _input.Camera.Zoom.ReadValue<float>();
			if (Mathf.Abs(zoomInput) > 0.01f) {
				_zoomLevel = Mathf.Clamp(_zoomLevel - zoomInput * _zoomSpeed * Time.deltaTime, _minZoom, _maxZoom);
				UpdateFollow();
			}
		}
		private void UpdateFollow() {
			_cinemachineFollow.FollowOffset = _positions[_currentMode] * _zoomLevel;
		}
		
		private void OnEnable() {
			_input.Camera.RotateRight.performed += RotateRight;
			_input.Camera.RotateLeft.performed += RotateLeft;
		}
		private void OnDisable() {
			_input.Camera.RotateRight.performed -= RotateRight;
			_input.Camera.RotateLeft.performed -= RotateLeft;
		}
	}
}