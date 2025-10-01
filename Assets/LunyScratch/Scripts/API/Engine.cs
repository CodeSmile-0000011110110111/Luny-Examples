// IScratchEngine.cs
using System;

namespace LunyScratch
{
	// Core engine interface
	public interface IScratchEngine
	{
		void Log(String message);
		void ShowMessage(String message, Single duration);
		void PlaySound(String soundName, Single volume);
		Single GetDeltaTime();
	}

	// Object manipulation interface
	public interface IEngineObject
	{
		void SetEnabled(Boolean enabled);
	}

	// Global engine accessor
	public static class Engine
	{
		private static IScratchEngine s_Instance;

		public static void Initialize(IScratchEngine engine) => s_Instance = engine;

		public static IScratchEngine Current => s_Instance ?? throw new Exception("Engine not initialized");
	}
}
