using System;
using UnityEngine;

namespace Core.UI.Popups {
	public abstract class AbstractPopupView: MonoBehaviour {
		public abstract Type ViewModelType { get; }
		
		public abstract void Init(IPositionUpdateEvent canvas, Camera targetCamera);
		public abstract void Bind(IPopupViewModel viewModel);
	}
}