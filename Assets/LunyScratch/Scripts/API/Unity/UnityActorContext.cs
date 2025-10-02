using System;
using UnityEngine;

namespace LunyScratch.Unity
{
	public sealed class UnityActorContext : IActorContext
	{
		private readonly MonoBehaviour _component;

		public IVector3 Forward => new UnityVector3(_component.transform.forward);
		public ITransform Transform => new UnityTransform(_component.transform);

		public UnityActorContext(MonoBehaviour component) => _component = component;

		public IGameEngineObject GetComponent(String componentName) => throw new NotImplementedException();

		// var component = _mono.GetComponentInChildren(componentName);
		// return component != null ? new UnityEngineObject(component) : null;

		public IGameEngineObject[] GetComponents(String componentName)
		{
			var components = _component.GetComponentsInChildren<Light>(); // Generic version needs type
			var result = new IGameEngineObject[components.Length];
			for (var i = 0; i < components.Length; i++)
				result[i] = new UnityEngineObject(components[i]);
			return result;
		}
	}
}
