using System;

namespace Core {
	public class ReactiveProperty<T> {
		private T _value;

		public T Value {
			set => SetValue(value);
			get => _value;
		}
		public event Action<T> ValueChanged;

		public void SetValue(T value) {
			_value = value;
			ValueChanged?.Invoke(value);
		}
	}
}