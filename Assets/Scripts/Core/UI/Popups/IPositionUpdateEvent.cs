using System;

namespace Core.UI.Popups {
	public interface IPositionUpdateEvent {
		event Action UpdatePosition;
	}
}