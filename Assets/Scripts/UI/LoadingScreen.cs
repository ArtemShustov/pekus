using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI {
	public class LoadingScreen: MonoBehaviour {
		[SerializeField] private RectTransform _leftSide;
		[SerializeField] private RectTransform _rightSide;
		[SerializeField] private GameObject _canvas;
		[SerializeField] private float _animationDuration = 2;

		public async Task ShowAsync() {
			_canvas.SetActive(true);
			_leftSide.DOAnchorPosX(0, _animationDuration).Play();
			_rightSide.DOAnchorPosX(0, _animationDuration).Play();
			await Awaitable.WaitForSecondsAsync(_animationDuration);
		}
		public async Task HideAsync() {
			_leftSide.DOAnchorPosX(-_leftSide.rect.width, _animationDuration).Play();
			_rightSide.DOAnchorPosX(_rightSide.rect.width, _animationDuration).Play();
			await Awaitable.WaitForSecondsAsync(_animationDuration);
			_canvas.SetActive(false);
		}
	}
}