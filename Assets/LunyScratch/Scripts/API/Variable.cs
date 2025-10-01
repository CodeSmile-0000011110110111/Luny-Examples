// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEditor;
using UnityEngine;

namespace LunyScratch
{
	public enum ValueType
	{
		Nil = 0,
		Boolean,
		Number,
		String,
	}

	public struct Variable
	{
		private ValueType m_ValueType;
		private Double m_Number;
		private String m_String;

		public Variable(Boolean truthValue)
		{
			m_ValueType = ValueType.Boolean;
			m_Number = truthValue ? 1 : 0;
			m_String = null;
		}

		public Variable(Double number)
		{
			m_ValueType = ValueType.Number;
			m_Number = number;
			m_String = null;
		}

		public Variable(String text)
		{
			m_ValueType = ValueType.String;
			m_Number = 0;
			m_String = text;
		}

		public Double AsNumber() => m_ValueType switch
		{
			ValueType.Boolean or ValueType.Number => m_Number,
			var _ => 0.0,
		};

		public String AsString() => m_ValueType switch
		{
			ValueType.Boolean or ValueType.Number => m_Number.ToString(),
			ValueType.String => m_String ?? String.Empty,
			var _ => String.Empty,
		};

		public Boolean AsBool() => m_ValueType switch
		{
			ValueType.Boolean or ValueType.Number => m_Number != 0,
			var _ => false,
		};

		public void Set(Double number)
		{
			m_ValueType = ValueType.Number;
			m_Number = number;
		}

		public void Set(Boolean truthValue)
		{
			m_ValueType = ValueType.Boolean;
			m_Number = truthValue ? 1 : 0;
		}

		public void Set(String text)
		{
			m_ValueType = ValueType.String;
			m_String = text;
		}

		public static implicit operator Variable(Int32 v) => new(v);
		public static implicit operator Variable(Single v) => new(v);
		public static implicit operator Variable(Boolean v) => new(v);
		public static implicit operator Variable(String v) => new(v);
	}
}
