using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LunyScratch.Unity
{
	public sealed class UnityEngineObject : IGameEngineObject
	{
		private readonly Object _obj;

		// Implicit conversion for convenience
		public static implicit operator UnityEngineObject(Object obj) => new(obj);

		public UnityEngineObject(Object obj) => _obj = obj;

		public void SetEnabled(Boolean enabled)
		{
			switch (_obj)
			{
				case Behaviour behaviour:
					behaviour.enabled = enabled;
					break;
				case GameObject go:
					go.SetActive(enabled);
					break;
			}
		}
	}
}
