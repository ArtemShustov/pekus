using System;
using System.Collections.Generic;

namespace Core.DependencyInjection {
	public class DIContainer {
		private readonly Dictionary<Type, DIEntry> _entries = new Dictionary<Type, DIEntry>();
		private readonly DIContainer _parent;
		private readonly HashSet<Type> _cache = new HashSet<Type>();
		
		public DIContainer(DIContainer parent = null) {
			_parent = parent;
		}
		
		public T Resolve<T>() {
			if (_cache.Contains(typeof(T))) {
				throw new Exception($"Cyclic dependency for {typeof(T)}");
			}
			_cache.Add(typeof(T));
			
			try {
				if (_entries.TryGetValue(typeof(T), out DIEntry entry)) {
					return entry.Resolve<T>(this);
				} 
				if (_parent != null) {
					return _parent.Resolve<T>();
				}
			} finally {
				_cache.Remove(typeof(T));
			}
			throw new Exception($"Could not resolve type {typeof(T)}");
		}

		public InstanceDIEntry<T> RegisterInstance<T>(T instance) {
			var entry = new InstanceDIEntry<T>(instance);
			_entries.Add(typeof(T), entry);
			return entry;
		}
		public FactoryDIEntry<T> RegisterFactory<T>(Func<DIContainer, T> factory) {
			var entry = new FactoryDIEntry<T>(factory);
			_entries.Add(typeof(T), entry);
			return entry;
		}
	}
}