using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	[DefaultExecutionOrder(Int16.MinValue)]
	public sealed class ScratchRuntime : MonoBehaviour
	{
		private static ScratchRuntime s_Instance;
		private readonly List<IStep> m_Steps = new();

		// Register a new step sequence
		public static void Run(IStep step)
		{
			step.OnEnter();
			s_Instance.m_Steps.Add(step);
		}

		// Runtime actions (called by steps)
		public static void Say(String message) => Debug.Log($"[Say] {message}");

		// TODO: Show speech bubble UI
		public static void PlaySound(String sound) => Debug.Log($"[Audio] {sound}");

		// TODO: Play audio clip
		public static void Move(Single x, Single y, Single z) => Debug.Log($"[Move] {x}, {y}, {z}");

		// TODO: Move player/object
		public static void StartStage(String stageName) => Debug.Log($"[Stage] Loading {stageName}");

		private void Awake()
		{
			if (s_Instance != null)
				throw new Exception("Only one instance of ScratchRuntime is allowed.");

			s_Instance = this;

			// Initialize the engine abstraction
			Engine.Initialize(new Unity.UnityScratchEngine());
		}

		private void Update()
		{
			// Execute all active FSM steps
			for (var i = m_Steps.Count - 1; i >= 0; i--)
			{
				var step = m_Steps[i];
				step.Execute();

				if (step.IsComplete())
				{
					step.OnExit();
					m_Steps.RemoveAt(i);
				}
			}
		}

		private void OnDestroy() => s_Instance = null;
	}
}
