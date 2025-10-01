// ScratchActor.cs

using System;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	// Engine-agnostic actor interface
	public interface IScratchActor
	{
		void OnStart();
	}

	// Actor context provides engine-specific data
	public interface IActorContext
	{
		IVector3 Forward { get; }
		ITransform Transform { get; }
		IEngineObject GetComponent(String componentName);
		IEngineObject[] GetComponents(String componentName);
	}

	// Math abstractions
	public interface IVector3
	{
		Single X { get; set; }
		Single Y { get; set; }
		Single Z { get; set; }
	}

	public interface ITransform
	{
		IVector3 Position { get; set; }
		IVector3 Forward { get; }
	}

	// Physics abstraction
	public interface IRigidbody
	{
		IVector3 LinearVelocity { get; set; }
		IVector3 Position { get; set; }
	}
}
