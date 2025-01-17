using System;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class InteractionPopup: Popup, IDisposable {
		private readonly ReactiveProperty<bool> _isSelected = new ReactiveProperty<bool>();
		private readonly string _text;
		
		public string Text => _text;
		public ReactiveProperty<bool> IsSelected => _isSelected;

		public event Action Triggered;

		public InteractionPopup(Transform target, string text): base(target) {
			_text = text;
		}
		public InteractionPopup(Transform target, Vector3 offset, string text): base(target, offset) {
			_text = text;
		}

		public void SetSelected(bool selected) {
			_isSelected.Value = selected;
		}
		public void Trigger() {
			Triggered?.Invoke();
		}
		
		public void Dispose() {
			Close();
		}
	}
}