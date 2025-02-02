using System;
using UnityEngine;

namespace Game.World {
	public class WorldClock: MonoBehaviour {
		[Range(0f, 1f)]
		[SerializeField] private float _nightThreshold = 0.7f;
		[SerializeField] private AnimationCurve _dayCurve;
		private WorldTime _current;
		
		public WorldTime Time => _current;
		public float NightTransition => _dayCurve.Evaluate((float)_current.Time / WorldTime.TimeInDay);
		public bool IsNight => NightTransition >= _nightThreshold;
		
		public event Action TimeChanged;
		
		private void Update() {
			if (Input.GetKeyDown(KeyCode.Keypad7)) {
				_current.Add(1);
				TimeChanged?.Invoke();
			}
			if (Input.GetKeyDown(KeyCode.Keypad8)) {
				_current.Add(10);
				TimeChanged?.Invoke();
			}
			if (Input.GetKeyDown(KeyCode.Keypad9)) {
				_current.Add(60);
				TimeChanged?.Invoke();
			}
		}

		public void Tick() {
			_current.AddTick();
			TimeChanged?.Invoke();
		}

		private void OnGUI() {
			var style = new GUIStyle();
			style.fontSize = 48;
			GUILayout.Label($"Day {_current.Day} - {_current.TimeToString()}. IsNight: {IsNight}", style);
		}
	}
}