using System;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class InteractionPopupViewModel: IPopupViewModel {
		private InteractionPopup _model;
		
		public Transform Target => _model.Target;
		public Vector3 Offset => _model.Offset;
		
		public string Text => _model.Text;
		public ReactiveProperty<bool> IsSelected => _model.IsSelected;
		
		public event Action CloseRequested {
			add => _model.CloseRequested += value;
			remove => _model.CloseRequested -= value;
		}
		public event Action Triggered {
			add => _model.Triggered += value;
			remove => _model.Triggered -= value;
		}

		public InteractionPopupViewModel(InteractionPopup model) {
			_model = model;
		}
	}
}