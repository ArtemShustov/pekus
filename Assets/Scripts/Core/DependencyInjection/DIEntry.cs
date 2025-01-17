using System;

namespace Core.DependencyInjection {
	public abstract class DIEntry<T>: DIEntry {
		public abstract T Resolve(DIContainer container);
	}

	public abstract class DIEntry {
		public virtual T Resolve<T>(DIContainer container) {
			if (this is DIEntry<T> entry) {
				return entry.Resolve(container);
			}
			throw new ArgumentException("Could not resolve type " + typeof(T));
		}
	}
}