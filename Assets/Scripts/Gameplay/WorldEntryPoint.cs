using System;
using System.Linq;
using System.Reflection;
using Core.Characters;
using Core.DependencyInjection;
using Core.UI.Popups;
using Game.GameEntry;
using UnityEngine;

namespace Game.Gameplay {
	public class WorldEntryPoint: MonoBehaviour {
		[Header("Components")]
		[SerializeField] private PopupCanvas _popupCanvas;
		[SerializeField] private GameCamera _camera;
		[Header("Settings")]
		[SerializeField] private Transform _playerSpawnPoint;
		[SerializeField] private WorldRoot _root;
		
		private PlayerCharacter _player;
		
		public void Run(GameContext gameContext, PlayerCharacter player) {
			// DI
			var container = new DIContainer(gameContext.Container);
			_root.SetContainer(container);
			container.RegisterInstance(_camera);
			container.RegisterInstance(_popupCanvas);
			
			InjectSceneObjects();
			
			// Init
			_player = player;
			_player.DisableInput();
			_player.transform.position = _playerSpawnPoint.position;
			
			_root.SetPlayer(_player);
			_camera.DisableInput();
		}
		public void PostInit() {
			_player.EnableInput();
			_camera.EnableInput();
		}

		private void InjectSceneObjects() {
			var objects = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
			foreach (var monoBehaviour in objects) {
				var fields = monoBehaviour.GetType()
					.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
					.Where(field => field.GetCustomAttribute<InjectAttribute>() != null);

				foreach (var field in fields) {
					var resolveMethod = typeof(DIContainer)
						.GetMethod(nameof(DIContainer.Resolve))
						?.MakeGenericMethod(field.FieldType);

					if (resolveMethod != null) {
						var dependency = resolveMethod.Invoke(_root.Container, null);
						field.SetValue(monoBehaviour, dependency);
						Debug.Log($"Injected {dependency.GetType().Name} into {field.Name} in {monoBehaviour.name}");
					} else {
						throw new Exception($"Cannot resolve dependency for field {field.Name} in {monoBehaviour.name}");
					}
				}
			}
		}
	}
}