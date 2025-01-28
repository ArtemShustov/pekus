using Core.Characters;
using Core.DependencyInjection;
using Core.UI.Popups.Variants;
using UnityEngine;

namespace Core.UI.Popups.Testings {
	public class TestShowPopup: MonoBehaviour {
		[SerializeField] private bool _selfTarget = false;
		[SerializeField] private Vector3 _offset = Vector3.zero;
		[SerializeField] private string _text = "Test text: {0}";
		[SerializeField] private int _aliveTime = 5;
		
		[Inject] private PopupCanvas _popupCanvas;
		private DialogPopup _popup;
		private int _counter;

		private void OnTriggerEnter(Collider other) {
			if (_popupCanvas == null) return;
			
			if (other.TryGetComponent<Character>(out var character)) {
				_counter++;
				_popup?.Dispose();
				
				_popup = new DialogPopup(
					_selfTarget ? transform : character.transform,
					_offset,
					string.Format(_text, _counter)
				);
				_popup.CloseAfterAsync(_aliveTime).Forget();
				var viewModel = new DialogPopupViewModel(_popup);
				_popupCanvas.Show(viewModel);
			}
		}

		private void OnDisable() {
			_popup?.Dispose();
		}
	}
}