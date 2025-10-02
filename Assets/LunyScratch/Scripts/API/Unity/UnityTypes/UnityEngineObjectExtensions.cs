using UnityEngine;

namespace LunyScratch.Unity
{
	public static class UnityEngineObjectExtensions
	{
		public static IGameEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);
	}
}
