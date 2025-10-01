using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	public class Sequence
	{
		// Run sequence of steps
		public static void Run(params IStep[] steps)
		{
			var sequence = new SequenceStep(new List<IStep>(steps));
			ScratchRuntime.Run(sequence);
		}

		// Repeat steps forever
		public static void RepeatForever(params IStep[] steps) => Run(new RepeatForeverStep(new List<IStep>(steps)));

		// Repeat steps while condition is true
		public static void RepeatWhileTrue(Func<Boolean> condition, params IStep[] steps) =>
			Run(new RepeatWhileTrueStep(condition, new List<IStep>(steps)));

		// Repeat steps until condition becomes true
		public static void RepeatUntilTrue(Func<Boolean> condition, params IStep[] steps) =>
			Run(new RepeatUntilTrueStep(condition, new List<IStep>(steps)));
	}
}
