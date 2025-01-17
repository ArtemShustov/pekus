using UnityEngine;

namespace Game.UI {
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class AlignBottom: MonoBehaviour {
		[SerializeField] private float _offset = 0;
		private RectTransform _rectTransform;

		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
			UpdatePosition();
		}
		private void Update() {
			UpdatePosition();
		}
		
		private void UpdatePosition() {
			var position = _rectTransform.anchoredPosition;
			position.y = _rectTransform.sizeDelta.y / 2 + _offset;
			_rectTransform.anchoredPosition = position;
		}
	}
}