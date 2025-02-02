using System;
using Core.DependencyInjection;
using Core.UI.Popups;
using Core.UI.Popups.Variants;
using Game.Gameplay;
using Game.Players;
using UnityEngine;

namespace Game.Interactions {
	public class Interaction: MonoBehaviour {
		[SerializeField] private string _text = "Interaction";
		[Inject] private PopupCanvas _popupCanvas;
		private InteractionPopup _popup;

		public event Action<Player> Triggered;

		private void Start() {
			if (_popup != null) {
				return;
			}
			_popup = new InteractionPopup(transform, _text);
			var viewModel = new InteractionPopupViewModel(_popup);
			_popupCanvas.Show(viewModel);
		}

		public virtual void Run(Player player) {
			_popup?.Trigger();
			Triggered?.Invoke(player);
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