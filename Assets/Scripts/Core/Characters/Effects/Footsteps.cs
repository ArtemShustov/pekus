using UnityEngine;

namespace Core.Characters.Effects {
	public class Footsteps: MonoBehaviour {
		[SerializeField] private float _pitch = 1;
		[Range(0, 1)]
		[SerializeField] private float _randomPitch = 0.2f;
		[SerializeField] private AudioSource _source;

		private void OnFootstepCallback() {
			_source.pitch = _pitch + UnityEngine.Random.Range(-_randomPitch, _randomPitch);
			_source.PlayOneShot(_source.clip);
		}
	}
}