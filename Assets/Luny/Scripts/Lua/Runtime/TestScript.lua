-- assign script's context table to a local variable (... is Lua's varargs operator):
local script = ...

-- Unity event messages call Lua functions of the same name in the 'script' table:
script.OnEnable = function()
	print("Hello, " .. script.Name .. ".lua")
end

script.Update = function()
    local x = script.transform.position.x
    script.transform.position = Vector3(x - 0.1 * Time.deltaTime, 0, 0)
end
