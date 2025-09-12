-- assign script's context table to a local variable:
local script = ...

-- Unity event messages call Lua functions of the same name in the 'script' table:
script.OnEnable = function()
	print("Hello, " .. script.Name .. ".lua on GameObject: " .. tostring(script.gameObject))
    print("Hello, " .. script.name .. ".lua with path: " .. script.path)

	print("name: " .. tostring(script.gameObject.name))
	print("layer: " .. tostring(script.gameObject.layer))
	print("transform: " .. tostring(script.gameObject.transform))
	print("transform: " .. tostring(script.transform))
end
