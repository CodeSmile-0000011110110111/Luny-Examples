// IScratchEngine.cs
using System;

namespace LunyScratch
{
	public static class GameEngine
	{
		private static IGameEngineActions s_Instance;

		public static void Initialize(IGameEngineActions actions) => s_Instance = actions;

		public static IGameEngineActions Current => s_Instance ?? throw new Exception("Scratch Engine not initialized");
	}
}
