using System;

namespace Core.DependencyInjection {
	public class FactoryDIEntry<T>: DIEntry<T> {
		private readonly Func<DIContainer, T> _factory;
		
		public FactoryDIEntry(Func<DIContainer, T> factory) {
			_factory = factory;
		}
		
		public override T Resolve(DIContainer container) {
			return _factory(container);
		}
	}
}