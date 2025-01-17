using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Core.UI.Popups {
	public class PopupCanvas: MonoBehaviour, IPositionUpdateEvent {
		[SerializeField] private CinemachineBrain _camera;
		[SerializeField] private Transform _container;
		[SerializeField] private AbstractPopupView[] _prefabs;
		private readonly Dictionary<Type, AbstractPopupView> _map = new Dictionary<Type, AbstractPopupView>();

		public event Action UpdatePosition;
		
		private void Awake() {
			foreach (var view in _prefabs) {
				_map.Add(view.ViewModelType, view);
			}
		}

		public void Show(IPopupViewModel viewModel) {
			if (_map.TryGetValue(viewModel.GetType(), out AbstractPopupView viewPrefab)) {
				var view = Instantiate(viewPrefab, _container);
				view.Init(this, _camera.OutputCamera);
				view.Bind(viewModel);
			} else {
				throw new Exception($"No popup view found for type: {viewModel.GetType()}");
			}
		}
		
		private void OnCameraUpdate(CinemachineBrain brain) {
			UpdatePosition?.Invoke();
		}

		private void OnEnable() {
			CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdate);
		}
		private void OnDisable() {
			CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdate);
		}
	}
}