using UnityEngine;

namespace LunyScratch.Unity
{
	/// <summary>
	/// Unity-specific extensions for ScratchActions that automatically wrap Unity objects.
	/// </summary>
	public static class UnityScratchActionsExt
	{
		public static IStep Enable(Object obj) => ScratchActions.Enable(new UnityEngineObject(obj));
		public static IStep Disable(Object obj) => ScratchActions.Disable(new UnityEngineObject(obj));
	}
}
