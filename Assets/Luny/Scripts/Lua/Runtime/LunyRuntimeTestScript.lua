-- assign script's context table to a local variable:
local script = ...

-- Unity event messages call Lua functions of the same name in the 'script' table:
script.OnEnable = function()
	print("Hello, " .. script.Name .. ".lua on GameObject: " .. tostring(script.gameObject))
end
