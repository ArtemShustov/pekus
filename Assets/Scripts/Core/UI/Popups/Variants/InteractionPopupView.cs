using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	[RequireComponent(typeof(RectTransform))]
	public class InteractionPopupView: PopupView<InteractionPopupViewModel> {
		[SerializeField] private Color _selectedColor = Color.yellow;
		[SerializeField] private Color _unselectedColor = Color.white;
		[SerializeField] private float _showAnimationDuration = 0.1f;
		[SerializeField] private float _triggeredAnimationDuration = 0.2f;
		[SerializeField] private TMP_Text _label;
		
		private RectTransform _rectTransform;

		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}

		public override void Bind(InteractionPopupViewModel viewModel) {
			base.Bind(viewModel);
			
			ViewModel.IsSelected.ValueChanged += OnSelectionChanged;
			ViewModel.Triggered += OnTriggered;
			_label.text = viewModel.Text;
			
			_rectTransform.localScale = new Vector3(1, 0, 1);
			_rectTransform.DOScaleY(1, _showAnimationDuration);
			OnUpdatePosition();
		}
		public override void Unbind(InteractionPopupViewModel viewModel) {
			ViewModel.IsSelected.ValueChanged -= OnSelectionChanged;
			ViewModel.Triggered -= OnTriggered;
			base.Unbind(viewModel);
		}

		private void OnTriggered() {
			_label.transform.localScale = Vector3.one;;
			_label.transform.DOPunchScale(new Vector3(0, 1.5f, 0), _triggeredAnimationDuration, 20);
		}
		private void OnSelectionChanged(bool isSelected) {
			_label.color = isSelected ? _selectedColor : _unselectedColor;
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