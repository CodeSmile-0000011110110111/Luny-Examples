// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	// Actor context provides engine-specific data
	public interface IActorContext
	{
		IVector3 Forward { get; }
		ITransform Transform { get; }
		IGameEngineObject GetComponent(String componentName);
		IGameEngineObject[] GetComponents(String componentName);
	}
}
