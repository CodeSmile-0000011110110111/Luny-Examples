// Base interface for FSM steps

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	public interface IStep
	{
		Boolean IsComplete();
		void Execute();
		void OnEnter();
		void OnExit();
	}

// Wait step (delays for N seconds)
	public sealed class WaitStep : IStep
	{
		private readonly Double _duration;
		private Double _elapsed;

		public WaitStep(Double duration)
		{
			_duration = duration;
			_elapsed = 0;
		}

		public void OnEnter() => _elapsed = 0;
		public void OnExit() {}

		public void Execute() => _elapsed += Time.deltaTime;

		public Boolean IsComplete() => _elapsed >= _duration;
	}

// Action step (executes immediately)
	public sealed class ActionStep : IStep
	{
		private readonly Action _action;
		private Boolean _executed;

		public ActionStep(Action action)
		{
			_action = action;
			_executed = false;
		}

		public void OnEnter() => _executed = false;

		public void Execute()
		{
			if (!_executed)
			{
				_action?.Invoke();
				_executed = true;
			}
		}

		public void OnExit() {}
		public Boolean IsComplete() => _executed;
	}

// Conditional step (checks condition before proceeding)
	public sealed class ConditionStep : IStep
	{
		private readonly Func<Boolean> _condition;
		private Boolean _result;

		public ConditionStep(Func<Boolean> condition)
		{
			_condition = condition;
			_result = false;
		}

		public void OnEnter() => _result = false;

		public void Execute() => _result = _condition();

		public void OnExit() {}
		public Boolean IsComplete() => _result;
	}

// Sequence of steps
	public sealed class SequenceStep : IStep
	{
		private readonly List<IStep> _steps;
		private Int32 _currentIndex;

		public SequenceStep(List<IStep> steps)
		{
			_steps = steps;
			_currentIndex = 0;
		}

		public void OnEnter()
		{
			_currentIndex = 0;
			if (_steps.Count > 0)
				_steps[0].OnEnter();
		}

		public void Execute()
		{
			if (_currentIndex >= _steps.Count) return;

			var currentStep = _steps[_currentIndex];
			currentStep.Execute();

			if (currentStep.IsComplete())
			{
				currentStep.OnExit();
				_currentIndex++;

				if (_currentIndex < _steps.Count)
					_steps[_currentIndex].OnEnter();
			}
		}

		public void OnExit()
		{
			if (_currentIndex < _steps.Count)
				_steps[_currentIndex].OnExit();
		}

		public Boolean IsComplete() => _currentIndex >= _steps.Count;
	}
}
