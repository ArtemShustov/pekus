using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	[RequireComponent(typeof(RectTransform))]
	public class DialogPopupView: PopupView<DialogPopupViewModel> {
		[SerializeField] private float _showAnimationDuration = 0.1f;
		[SerializeField] private TMP_Text _label;
		
		private RectTransform _rectTransform;

		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}

		public override void Bind(DialogPopupViewModel viewModel) {
			base.Bind(viewModel);
			
			_label.text = viewModel.Text;

			_rectTransform.localScale = new Vector3(1, 0, 1);
			_rectTransform.DOScaleY(1, _showAnimationDuration);
			OnUpdatePosition();
		}
		protected override void OnUpdatePosition() {
			var position = Camera.WorldToScreenPoint(ViewModel.Target.position + ViewModel.Offset);
			position.z = 0;
			_rectTransform.position = position;
		}

		protected override void OnCloseRequested() {
			Destroy(gameObject);
		}
	}
}