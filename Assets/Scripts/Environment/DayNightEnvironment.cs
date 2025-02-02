using Core.DependencyInjection;
using Game.World;
using UnityEngine;

namespace Game.Environment {
	public class DayNightEnvironment : MonoBehaviour {
		[Header("Settings")]
		[SerializeField] private AnimationCurve _ambientIntensity;
		[SerializeField] private AnimationCurve _sunIntensity;
		[Header("Components")]
		[SerializeField] private Light _directionalLight;
		
		[Inject] private WorldRoot _world; 
		private Material _skyboxMaterial;
		private float _time;

		private readonly static int CubemapTransition = Shader.PropertyToID("_CubemapTransition");

		private void Start() {
			_world.Clock.TimeChanged += OnTimeChanged;
			if (_skyboxMaterial == null) {
				_skyboxMaterial = RenderSettings.skybox;
				_skyboxMaterial = new Material(_skyboxMaterial);
				RenderSettings.skybox = _skyboxMaterial;
			}
			OnTimeChanged();
		}
		private void UpdateLighting() {
			_skyboxMaterial.SetFloat(CubemapTransition, _time);
			_directionalLight.intensity = _sunIntensity.Evaluate(_time);
			RenderSettings.ambientIntensity = _ambientIntensity.Evaluate(_time);
		}
		
		private void OnTimeChanged() {
			_time = _world.Clock.NightTransition;
			UpdateLighting();
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
