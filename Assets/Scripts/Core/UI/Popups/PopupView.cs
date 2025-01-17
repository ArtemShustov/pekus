using System;
using UnityEngine;

namespace Core.UI.Popups {
	public abstract class PopupView<T>: AbstractPopupView where T: IPopupViewModel {
		private IPositionUpdateEvent _positionUpdate;
		private Camera _camera;
		private T _viewModel;
			
		public override Type ViewModelType => typeof(T);
		protected Camera Camera => _camera;
		protected T ViewModel => _viewModel;
		
		public override void Init(IPositionUpdateEvent canvas, Camera targetCamera) {
			if (_positionUpdate != null) {
				_positionUpdate.UpdatePosition -= OnUpdatePosition;
			} 
			_positionUpdate = canvas;
			_positionUpdate.UpdatePosition += OnUpdatePosition;
			_camera = targetCamera;
		}

		public override void Bind(IPopupViewModel viewModel) {
			if (viewModel is not T tViewModel) {
				throw new ArgumentException($"Argument 'viewModel' must be of type '{typeof(T)}' or derived from '{typeof(T)}'.");
			}
			
			if (_viewModel != null) {
				Unbind(_viewModel);
			}
			_viewModel = tViewModel;
			Bind(tViewModel);
		}
		public virtual void Bind(T viewModel) {
			_viewModel.CloseRequested += OnCloseRequested;
		}
		public virtual void Unbind(T viewModel) {
			_viewModel.CloseRequested -= OnCloseRequested;
			_viewModel = default; 
		}

		protected abstract void OnCloseRequested();
		protected abstract void OnUpdatePosition();
		protected virtual void OnDestroy() {
			if (_positionUpdate != null) {
				_positionUpdate.UpdatePosition -= OnUpdatePosition;
			}
			if (_viewModel != null) {
				Unbind(_viewModel);
			}
		}
	}
}