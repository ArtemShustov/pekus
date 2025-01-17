using System;
using UnityEngine;

namespace Core.Characters {
	public class CharacterModel: MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private string _isMoveParam = "IsMove";
		[SerializeField] private string _isRunParam = "IsRun";
		[Header("Components")]
		[SerializeField] private Animator _animator;

		public event Action<Vector3> AnimatorMove;
		
		private void OnAnimatorMove() {
			AnimatorMove?.Invoke(_animator.deltaPosition);
		}

		public void SetIsMove(bool isMove) {
			_animator.SetBool(_isMoveParam, isMove);
		}
		public void SetIsRun(bool isRun) {
			_animator.SetBool(_isRunParam, isRun);
		}
	}
}