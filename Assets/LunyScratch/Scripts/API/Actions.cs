// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	public static class Actions
	{
		public static IStep Say(String message, Double duration = 0) => new ActionStep(() =>
		{
			Debug.Log($"[Say] {message}");
			// Show UI, etc.
		});

		public static IStep PlaySound(String soundName, Double volume = 1.0f) => new ActionStep(() =>
		{
			Debug.Log($"[PlaySound] {soundName}");
			// Play sound implementation
		});

		public static IStep Wait(Double seconds) => new WaitStep(seconds);

		public static IStep Disable(UnityEngine.Object obj)
		{
			if (obj is Behaviour behaviour)
				return new ActionStep(() => behaviour.enabled = false);
			if (obj is GameObject go)
				return new ActionStep(() => go.SetActive(false));

			return null;
		}

		public static IStep Enable(UnityEngine.Object obj)
		{
			if (obj is Behaviour behaviour)
				return new ActionStep(() => behaviour.enabled = true);
			if (obj is GameObject go)
				return new ActionStep(() => go.SetActive(true));

			return null;
		}


		public static IStep RepeatForever(params IStep[] steps) => new RepeatForeverStep(new List<IStep>(steps));
		public static IStep RepeatForever(Action step)
		{
			var steps = new List<IStep>();
			steps.Add(new ActionStep(step));
			return new RepeatForeverStep(steps);
		}

		// Repeat steps while condition is true
		public static IStep RepeatWhileTrue(Func<Boolean> condition, params IStep[] steps) =>
			new RepeatWhileTrueStep(condition, new List<IStep>(steps));

		// Repeat steps until condition becomes true
		public static IStep RepeatUntilTrue(Func<Boolean> condition, params IStep[] steps) =>
			new RepeatUntilTrueStep(condition, new List<IStep>(steps));
	}
}
