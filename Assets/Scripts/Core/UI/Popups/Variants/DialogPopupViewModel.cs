using System;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class DialogPopupViewModel: IPopupViewModel {
		private DialogPopup _model;
		
		public string Text => _model.Text;
		public Transform Target => _model.Target;
		public Vector3 Offset => _model.Offset;
		public event Action CloseRequested {
			add => _model.CloseRequested += value;
			remove => _model.CloseRequested -= value;
		}

		public DialogPopupViewModel(DialogPopup model) {
			_model = model;
		}
	}
}