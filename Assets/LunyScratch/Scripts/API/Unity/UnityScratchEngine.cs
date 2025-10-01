// UnityScratchEngine.cs

using System;
using UnityEditor;
using UnityEngine;

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

}
