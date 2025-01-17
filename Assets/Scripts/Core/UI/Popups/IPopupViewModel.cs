using System;

namespace Core.UI.Popups {
	public interface IPopupViewModel {
		event Action CloseRequested;
	}
}