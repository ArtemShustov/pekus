using System;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class IconPopupViewModel: IPopupViewModel {
		private readonly IconPopup _model;
		
		public Sprite Icon => _model.Icon;
		public Transform Target => _model.Target;
		public Vector3 Offset => _model.Offset;
		public event Action CloseRequested {
			add => _model.CloseRequested += value;
			remove => _model.CloseRequested -= value;
		}

		public IconPopupViewModel(IconPopup model) {
			_model = model;
		}
	}
}