namespace Core.DependencyInjection {
	public class InstanceDIEntry<T>: DIEntry<T> {
		private readonly T _instance;
		
		public InstanceDIEntry(T instance) {
			_instance = instance;
		}
		
		public override T Resolve(DIContainer container) => _instance;
	}
}