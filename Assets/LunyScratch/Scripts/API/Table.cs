// Variable wrapper (weakly typed, like Scratch)

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	// Table (array + dictionary hybrid, like Lua)
	public class Table
	{
		private readonly List<Variable> m_Array = new();
		private readonly Dictionary<String, Variable> m_Dictionary = new();

		// Array operations (1-indexed like Scratch/Lua)
		public void Add(Variable value) => m_Array.Add(value);
		public Variable Get(Int32 index)
		{
			if (index < 0 || index >= m_Array.Count)
				return new Variable(0);

			return m_Array[index - 1];
			// 1-indexed!
		}

		public void Set(Int32 index, Variable value) => m_Array[index - 1] = value;
		public Int32 Length() => m_Array.Count;

		// Dictionary operations
		public Variable Get(String key) => m_Dictionary.TryGetValue(key, out var v) ? v : default;
		public void Set(String key, Variable value) => m_Dictionary[key] = value;
		public Boolean Has(String key) => m_Dictionary.ContainsKey(key);
	}
}
