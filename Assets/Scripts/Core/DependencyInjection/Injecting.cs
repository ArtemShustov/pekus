using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.DependencyInjection {
	public static class Injecting {
		public const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		public static void InjectForChildren(GameObject gameObject, DIContainer container) {
			var objects = gameObject.GetComponentsInChildren<MonoBehaviour>();
			
			foreach (var instance in objects) {
				foreach (var field in GetAllFields(instance)) {
					Inject(instance, field, container);
				}
			}
		}
		public static void InjectAllOnScene(DIContainer container) {
			var objects = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			
			foreach (var instance in objects) {
				foreach (var field in GetAllFields(instance)) {
					Inject(instance, field, container);
				}
			}
		}
		public static void Inject(MonoBehaviour instance, DIContainer container) {
			foreach (var field in GetAllFields(instance)) {
				Inject(instance, field, container);
			}
		} 
		public static void Inject(MonoBehaviour instance, FieldInfo field, DIContainer container) {
			var resolveMethod = typeof(DIContainer)
				.GetMethod(nameof(DIContainer.Resolve))
				?.MakeGenericMethod(field.FieldType);

			if (resolveMethod != null) {
				var dependency = resolveMethod.Invoke(container, null);
				field.SetValue(instance, dependency);
			} else {
				throw new Exception($"Cannot resolve dependency for field {field.Name} in {instance.name}");
			}
		}
		
		private static IEnumerable<FieldInfo> GetAllFields(MonoBehaviour instance) {
			var type = instance.GetType();
			while (type != typeof(object) && type != null) {
				foreach (var fieldInfo in type.GetFields(Flags)) {
					if (fieldInfo.GetCustomAttribute<InjectAttribute>() != null) {
						yield return fieldInfo;
					}
				}
				type = type?.BaseType;
			}
		}
	}
}