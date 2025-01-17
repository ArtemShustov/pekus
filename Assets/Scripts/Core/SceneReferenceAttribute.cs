using System;
using UnityEngine;

namespace Core {
	/// <summary>
	/// An attribute that checks whether a scene is in an assembly.
	/// </summary>
	/// <remarks>
	/// Applies only to string type.
	/// </remarks>
	/// <example>
	/// For inspector fields.
	/// <code>
	/// [SceneReference, SerializeField] private string _sceneName;
	/// </code>
	/// For constants, a warning will be displayed in the log when the assembly is reloaded.
	/// <code>
	/// [SceneReference] private const string _sceneName = "SCENE NAME";
	/// </code>
	/// </example>
	[AttributeUsage(AttributeTargets.Field)]
	public class SceneReferenceAttribute: PropertyAttribute {
		
	}
}