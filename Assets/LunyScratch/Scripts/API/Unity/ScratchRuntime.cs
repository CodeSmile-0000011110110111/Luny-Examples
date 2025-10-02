using LunyScratch.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	[AddComponentMenu("GameObject/")] // Do not list in "Add Component" menu
	[DisallowMultipleComponent]
	public sealed class ScratchRuntime : MonoBehaviour
	{
		private static ScratchRuntime s_Instance;
		private static bool s_Initialized;

		private readonly List<IStep> _steps = new();

		// Self-initializing singleton property
		public static ScratchRuntime Instance
		{
			get
			{
				if (s_Initialized == false)
				{
					// Create a new GameObject with ScratchRuntime component
					var go = new GameObject("ScratchRuntime");
					s_Instance = go.AddComponent<ScratchRuntime>();
					s_Initialized = true;
					DontDestroyOnLoad(go);

					// Initialize the engine abstraction
					GameEngine.Initialize(new UnityEngineActions(s_Instance));
				}
				return s_Instance;
			}
		}

		// Register a new step sequence
		public void RunStep(IStep step)
		{
			step.OnEnter();
			Instance._steps.Add(step);
		}

		private void Awake()
		{
			if (s_Instance != null)
				throw new Exception($"{nameof(ScratchRuntime)}: duplicate singleton detected, remove {nameof(ScratchRuntime)} from scene as it will be autocreated");
		}

		private void Update()
		{
			// Execute all active FSM steps
			for (var i = _steps.Count - 1; i >= 0; i--)
			{
				var step = _steps[i];
				step.Execute();

				if (step.IsComplete())
				{
					step.OnExit();
					_steps.RemoveAt(i);
				}
			}
		}

		private void OnDestroy()
		{
			s_Instance = null;
			s_Initialized = false;
		}
	}
}
