-- assign script's context table to a local variable:
local script = ...

script.OnScriptUnload = function()
    print("OnScriptUnload: " .. tostring(Time.realtimeSinceStartupAsDouble))
end
script.OnScriptLoad = function()
    print("OnScriptLoad: " .. tostring(Time.realtimeSinceStartupAsDouble))
end

script.Awake = function()
    print("Awake: " .. tostring(Time.realtimeSinceStartupAsDouble))
end

-- Unity event messages call Lua functions of the same name in the 'script' table:
script.OnEnable = function()
    print("OnEnable: " .. tostring(Time.realtimeSinceStartupAsDouble))

	print("Hello, " .. script.Name .. ".lua on GameObject: " .. tostring(script.gameObject))
    print("Hello, " .. script.name .. ".lua with path: " .. script.path)

	print("name: " .. tostring(script.gameObject.name))
	print("layer: " .. tostring(script.gameObject.layer))
	print("script.transform: " .. tostring(script.transform))
	print("script.gameObject.transform: " .. tostring(script.gameObject.transform))
	print("script.gameObject:GetComponent(Transform): " .. tostring(script.gameObject:GetComponent(Transform)))

	print("properties identical? " .. tostring(script.transform == script.gameObject.transform))
	print("GetComponent identical? " .. tostring(script.gameObject:GetComponent(Transform) == script.gameObject.transform))
end

script.Start = function()
    print("Start: " .. tostring(Time.realtimeSinceStartupAsDouble))
end

script.OnDisable = function()
    print("OnDisable: " .. tostring(Time.realtimeSinceStartupAsDouble))
end

script.OnDestroy = function()
    print("OnDestroy: " .. tostring(Time.realtimeSinceStartupAsDouble))
end
