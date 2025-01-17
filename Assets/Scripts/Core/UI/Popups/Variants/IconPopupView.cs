using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.Popups.Variants {
	public class IconPopupView: PopupView<IconPopupViewModel> {
		[SerializeField] private float _showAnimationDuration = 0.1f;
		[SerializeField] private Image _image;
		
		private RectTransform _rectTransform;

		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}

		public override void Bind(IconPopupViewModel viewModel) {
			base.Bind(viewModel);
			
			_image.sprite = viewModel.Icon;

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