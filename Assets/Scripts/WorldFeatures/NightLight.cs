using Core.DependencyInjection;
using Game.Gameplay;
using UnityEngine;

namespace Game.WorldFeatures {
	public class NightLight: MonoBehaviour {
		[SerializeField] private Light _light;
		[Inject] private World _world;

		private void Start() {
			_world.Clock.TimeChanged += OnTimeChanged;
			OnTimeChanged();
		}
		
		private void OnTimeChanged() {
			_light.enabled = _world.Clock.IsNight;
		}
		private void OnEnable() {
			if (_world) {
				_world.Clock.TimeChanged += OnTimeChanged;
			}
		}
		private void OnDisable() {
			if (_world) {
				_world.Clock.TimeChanged -= OnTimeChanged;
			}
		}
	}
}