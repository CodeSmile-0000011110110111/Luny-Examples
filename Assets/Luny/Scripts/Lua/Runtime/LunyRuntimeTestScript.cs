using Lua;
using Luny;
using UnityEngine;

public sealed class LunyRuntimeTestScript : LunyRuntimeScript
{
	// To get/set script variables when the script is first loaded:
	protected override void OnBeforeInitialScriptLoad(LuaTable scriptContext)
	{
	}

	// To get/set script variables every time the script is loaded (incl. hot reload):
	protected override void OnBeforeScriptLoad(LuaTable scriptContext) 
	{
		Debug.Log($"gameObject is: {scriptContext["gameObject"]}");
	}

	// To get/set script variables every time after the script was loaded (incl. hot reload):
	protected override void OnAfterScriptLoad(LuaTable scriptContext)
	{
	}
}
