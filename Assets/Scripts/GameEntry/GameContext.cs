using Core.DependencyInjection;

namespace Game.GameEntry {
	public class GameContext {
		public DIContainer Container { get; private set; } = new DIContainer();
	}
}