using System.Text;
using UnityEngine;

namespace Core.Characters {
	public class CharacterDebugger: MonoBehaviour {
		[SerializeField] private CharacterStateMachine _machine;
		private readonly GUIStyle _style = new GUIStyle();

		private void Awake() {
			_style.alignment = TextAnchor.UpperCenter;
		}

		private void OnGUI() {
			_style.fontSize = Mathf.RoundToInt(Screen.height * 0.05f);
			var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			var rect = new Rect(screenPosition, Vector2.zero);
			rect.y = Screen.height - screenPosition.y;
			var text = new StringBuilder();
			// 
			text.AppendLine($"=[ Character '{_machine.name}' ]=");
			text.AppendLine($"State: {_machine.State?.GetType()}");
			text.AppendLine($"Input: {_machine.Input.Movement} ({_machine.Input.Movement.magnitude})");
			// 
			GUI.Label(rect, text.ToString(), _style);
		}
	}
}