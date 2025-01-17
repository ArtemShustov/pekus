using Core.DependencyInjection;
using Core.UI.Popups.Variants;
using UnityEngine;

namespace Core.UI.Popups.Testings {
	public class TestIconPopup: MonoBehaviour {
		[SerializeField] private Vector3 _offset;
		[SerializeField] private Sprite _icon;
		[Inject] private PopupCanvas _popupCanvas;
		private IconPopup _popup;
		
		private void Start() {
			Show();
		}

		public void Init(PopupCanvas popupCanvas) {
			_popupCanvas = popupCanvas;
		}

		private void Show() {
			if (_popup != null) {
				_popup.Dispose();
			}
			_popup = new IconPopup(transform, _offset, _icon);
			var viewModel = new IconPopupViewModel(_popup);
			_popupCanvas.Show(viewModel);
		}
	}
}