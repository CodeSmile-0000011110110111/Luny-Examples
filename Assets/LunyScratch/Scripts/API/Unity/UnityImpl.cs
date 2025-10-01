// UnityActorAdapter.cs

using System;
using UnityEditor;
using UnityEngine;

namespace LunyScratch.Unity
{
	// Unity implementations of abstractions
	public class UnityVector3 : IVector3
	{
		private Vector3 _value;

		public Single X { get => _value.x; set => _value.x = value; }
		public Single Y { get => _value.y; set => _value.y = value; }
		public Single Z { get => _value.z; set => _value.z = value; }
		public static implicit operator UnityVector3(Vector3 v) => new(v);
		public static implicit operator Vector3(UnityVector3 v) => v._value;

		public UnityVector3(Vector3 value) => _value = value;

		public Vector3 ToUnity() => _value;
	}

	public class UnityTransform : ITransform
	{
		private readonly Transform _transform;

		public IVector3 Position
		{
			get => new UnityVector3(_transform.position);
			set => _transform.position = ((UnityVector3)value).ToUnity();
		}

		public IVector3 Forward => new UnityVector3(_transform.forward);

		public UnityTransform(Transform transform) => _transform = transform;
	}

	public class UnityRigidbody : IRigidbody
	{
		private readonly Rigidbody _rigidbody;

		public IVector3 LinearVelocity
		{
			get => new UnityVector3(_rigidbody.linearVelocity);
			set => _rigidbody.linearVelocity = ((UnityVector3)value).ToUnity();
		}

		public IVector3 Position
		{
			get => new UnityVector3(_rigidbody.position);
			set => _rigidbody.position = ((UnityVector3)value).ToUnity();
		}

		public UnityRigidbody(Rigidbody rigidbody) => _rigidbody = rigidbody;
	}

	public class UnityActorContext : IActorContext
	{
		private readonly MonoBehaviour _mono;

		public IVector3 Forward => new UnityVector3(_mono.transform.forward);
		public ITransform Transform => new UnityTransform(_mono.transform);

		public UnityActorContext(MonoBehaviour mono) => _mono = mono;

		public IEngineObject GetComponent(String componentName)
		{
			throw new NotImplementedException();
			// var component = _mono.GetComponentInChildren(componentName);
			// return component != null ? new UnityEngineObject(component) : null;
		}

		public IEngineObject[] GetComponents(String componentName)
		{
			var components = _mono.GetComponentsInChildren<Light>(); // Generic version needs type
			var result = new IEngineObject[components.Length];
			for (var i = 0; i < components.Length; i++)
				result[i] = new UnityEngineObject(components[i]);
			return result;
		}
	}
}
