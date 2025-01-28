using System;
using Core.Characters;
using Core.DependencyInjection;
using Core.UI.Popups;
using Core.UI.Popups.Variants;
using UnityEngine;

namespace Core.Interactions {
	public class Interaction: MonoBehaviour {
		[SerializeField] private string _text = "Interaction";
		[Inject] private PopupCanvas _popupCanvas;
		private InteractionPopup _popup;

		public event Action<Character> Triggered;

		private void Start() {
			if (_popup != null) {
				return;
			}
			_popup = new InteractionPopup(transform, _text);
			var viewModel = new InteractionPopupViewModel(_popup);
			_popupCanvas.Show(viewModel);
		}

		public virtual void Run(Character character) {
			_popup?.Trigger();
			Triggered?.Invoke(character);
		}
		public void SetSelected(bool isSelected) {
			_popup?.SetSelected(isSelected);
		}

		protected virtual void OnEnable() {
			if (!_popupCanvas) {
				return;
			}
			_popup = new InteractionPopup(transform, _text);
			var viewModel = new InteractionPopupViewModel(_popup);
			_popupCanvas.Show(viewModel);
		}
		protected virtual  void OnDisable() {
			_popup?.Dispose();
		}
	}
}