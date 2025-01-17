using System;
using UnityEngine;

namespace Core.UI.Popups {
	public abstract class Popup {
		private readonly Transform _target;
		private readonly Vector3 _offset;

		public Transform Target => _target;
		public Vector3 Offset => _offset;

		public event Action CloseRequested;
		
		public Popup(Transform target) {
			_target = target;
			_offset = Vector3.zero;
		}
		public Popup(Transform target, Vector3 offset) {
			_target = target;
			_offset = offset;
		}

		public virtual void Close() => CloseRequested?.Invoke();
	}
}