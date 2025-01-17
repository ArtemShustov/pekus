using System;
using Core;
using Core.Characters;
using Core.Interactions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Testing {
	[Obsolete]
	[RequireComponent(typeof(Interaction))]
	public class LoadSceneInteraction: MonoBehaviour {
		[SceneReference]
		[SerializeField] private string _sceneName;
		private Interaction _interaction;

		private void Awake() {
			_interaction = GetComponent<Interaction>();
		}
		
		private void OnInteractionTriggered(Character obj) {
			Debug.Log($"LoadSceneInteraction: loading {_sceneName}");
			SceneManager.LoadScene(_sceneName);
		}
		private void OnEnable() {
			_interaction.Triggered += OnInteractionTriggered;
		}
		private void OnDisable() {
			_interaction.Triggered -= OnInteractionTriggered;
		}
	}
}