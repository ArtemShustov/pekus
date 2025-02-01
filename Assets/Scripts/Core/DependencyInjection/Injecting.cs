using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.DependencyInjection {
	public static class Injecting {
		private static Dictionary<Type, FieldInfo[]> _fieldsMap = new();
		private static Dictionary<Type, MethodInfo> _resolveMap = new();
		private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		#region GameObjects
		public static void InjectTree(GameObject gameObject, DIContainer container) {
			var list = new Queue<GameObject>();
			list.Enqueue(gameObject);

			while (list.Count > 0) {
				var obj = list.Dequeue();
				obj.Inject(container);
				foreach (Transform child in obj.transform) {
					list.Enqueue(child.gameObject);
				}
			}
		}
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
		#endregion
		
		#region MonoBehaviour
		public static void Inject(MonoBehaviour instance, DIContainer container) {
			foreach (var field in GetAllFields(instance)) {
				Inject(instance, field, container);
			}
		} 
		private static void Inject(MonoBehaviour instance, FieldInfo field, DIContainer container) {
			var resolveMethod = GetResolveMethod(field.FieldType);

			if (resolveMethod != null) {
				var dependency = resolveMethod.Invoke(container, null);
				field.SetValue(instance, dependency);
			} else {
				throw new Exception($"Cannot resolve dependency for field {field.Name} in {instance.name}");
			}
		}
		#endregion

		#region Common
		public static void Inject(object instance, DIContainer container) {
			foreach (var field in GetAllFields(instance)) {
				Inject(instance, field, container);
			}
		}
		private static void Inject(object instance, FieldInfo field, DIContainer container) {
			var resolveMethod = GetResolveMethod(field.FieldType);

			if (resolveMethod != null) {
				var dependency = resolveMethod.Invoke(container, null);
				field.SetValue(instance, dependency);
			} else {
				throw new Exception($"Cannot resolve dependency for field {field.Name} in {instance.GetType().Name}");
			}
		}
		#endregion
		
		private static IEnumerable<FieldInfo> GetAllFields(object instance) {
			var type = instance.GetType();

			if (!_fieldsMap.ContainsKey(type)) {
				BakeFields(instance);
			}
			
			foreach (var fieldInfo in _fieldsMap[type]) {
				yield return fieldInfo;
			}
		}
		private static void BakeFields(object instance) {
			var list = new List<FieldInfo>();
			var type = instance.GetType();
			while (type != typeof(object) && type != null) {
				foreach (var fieldInfo in type.GetFields(Flags)) {
					if (fieldInfo.GetCustomAttribute<InjectAttribute>() != null) {
						list.Add(fieldInfo);
					}
				}
				type = type?.BaseType;
			}
			_fieldsMap[instance.GetType()] = list.ToArray();
		}
		private static MethodInfo GetResolveMethod(Type fieldType) {
			if (_resolveMap.TryGetValue(fieldType, out var method)) {
				return method;
			}
			method = typeof(DIContainer)
				.GetMethod(nameof(DIContainer.Resolve))
				?.MakeGenericMethod(fieldType);
			_resolveMap[fieldType] = method;
			return method;
		}
	}
}