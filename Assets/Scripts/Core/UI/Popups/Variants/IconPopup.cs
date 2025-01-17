using System;
using UnityEngine;

namespace Core.UI.Popups.Variants {
	public class IconPopup: Popup, IDisposable {
		private readonly Sprite _icon;
		
		public Sprite Icon => _icon;

		public IconPopup(Transform target, Sprite icon): base(target) {
			_icon = icon;
		}
		public IconPopup(Transform target, Vector3 offset, Sprite icon): base(target, offset) {
			_icon = icon;
		}

		public void Dispose() {
			Close();
		}
	}
}