-- assign script's context table to a local variable:
local script = ...

-- Unity event messages call Lua functions of the same name in the 'script' table:
script.OnEnable = function()
	print("Hello, " .. script.Name .. ".lua")

	script.obj = GameObject.Find("Square")
end

script.Update = function()
    local transform = script.obj.transform
    transform.position = Vector3(transform.position.x + 0.1 * Time.deltaTime, 0, 0)
end
