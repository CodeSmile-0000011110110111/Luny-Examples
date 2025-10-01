// UnityScratchEngine.cs

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LunyScratch.Unity
{
	public class UnityScratchEngine : IScratchEngine
	{
		public void Log(String message) => Debug.Log(message);

		public void ShowMessage(String message, Single duration) => Debug.Log($"[Say] {message}");

		// TODO: Show speech bubble UI
		public void PlaySound(String soundName, Single volume) => Debug.Log($"[PlaySound] {soundName} @ {volume}");

		// TODO: Play audio clip
		public Single GetDeltaTime() => Time.deltaTime;
	}

	// Wrapper for Unity objects
	public class UnityEngineObject : IEngineObject
	{
		private readonly Object _obj;

		// Implicit conversion for convenience
		public static implicit operator UnityEngineObject(Object obj) => new(obj);

		public UnityEngineObject(Object obj) => _obj = obj;

		public void SetEnabled(Boolean enabled)
		{
			switch (_obj)
			{
				case Behaviour behaviour:
					behaviour.enabled = enabled;
					break;
				case GameObject go:
					go.SetActive(enabled);
					break;
			}
		}
	}

	namespace LunyScratch.Unity
	{
		public static class UnityExtensions
		{
			public static IEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);

			// Usage: Enable(lights[0].AsEngineObject())
		}
	}
}
