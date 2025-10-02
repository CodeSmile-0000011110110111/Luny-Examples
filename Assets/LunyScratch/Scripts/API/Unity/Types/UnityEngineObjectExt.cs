using UnityEngine;

namespace LunyScratch.Unity
{
	public static class UnityEngineObjectExt
	{
		public static IGameEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);
	}
}
