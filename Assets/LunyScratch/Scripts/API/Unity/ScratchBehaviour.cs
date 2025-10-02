using UnityEngine;

namespace LunyScratch
{
	/// <summary>
	/// Base class for all Scratch-style behaviors.
	/// Automatically initializes ScratchRuntime on first use.
	/// </summary>
	public abstract class ScratchBehaviour : MonoBehaviour
	{
		protected virtual void Awake()
		{
			// Trigger ScratchRuntime initialization
			var _ = UnityScratchRuntime.Instance;
		}
	}
}
