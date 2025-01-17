using UnityEngine;

namespace Core.Characters {
	public class InputContainer: ICharacterInput {
		private ICharacterInput _input;
			
		public Vector2 Movement => _input?.Movement ?? Vector2.zero;

		public void Change(ICharacterInput input) {
			if (_input != null) {
				UnsubscribeAll();
			}
			_input = input;
			if (_input != null) {
				SubscribeAll();
			}
		}
			
		private void SubscribeAll() {}
		private void UnsubscribeAll() {}
	}
}