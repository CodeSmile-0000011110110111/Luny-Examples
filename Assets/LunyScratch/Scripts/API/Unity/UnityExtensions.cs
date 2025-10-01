// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEditor;
using UnityEngine;

namespace LunyScratch.Unity.LunyScratch.Unity
{
	public static class UnityExtensions
	{
		public static IEngineObject AsEngineObject(this Object obj) => new UnityEngineObject(obj);

		// Usage: Enable(lights[0].AsEngineObject())
	}
}
