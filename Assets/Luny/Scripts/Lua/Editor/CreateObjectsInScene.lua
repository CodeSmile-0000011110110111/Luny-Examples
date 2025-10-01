local script = ...

script.OnScriptLoad = function()
    print("(Re-)Load script: " .. script.Name)
end

--[[
script.OnEnable = function()
    local rootObjectName = "Luny Test"
    local companionCubePath = "Assets/Luny/Prefabs/CompanionCube.prefab"

    -- re-create our test root object every time
    local rootObject = GameObject.Find(rootObjectName)
    if rootObject then
        Object.DestroyImmediate(rootObject)
    end
    rootObject = GameObject(rootObjectName)

    -- define chances of primitive types
    local primitives =
    {
        { chance = 10, type = PrimitiveType.Capsule },
        { chance = 20, type = PrimitiveType.Cylinder },
        { chance = 40, type = PrimitiveType.Sphere },
        { chance = 60, type = PrimitiveType.Cube },
        { chance = 100, type = AssetDatabase.LoadAssetAtPath(companionCubePath, GameObject) },
    }

    local max = { x = 12, y = 1, z = 12 }

    for x = 1, max.x do
        for y = 1, max.y do
            for z = 1, max.z do
                local primitiveType = PrimitiveType.Quad
                local randomValue = math.random(100)

                for i, primitive in ipairs(primitives) do
                    if randomValue <= primitive.chance then
                        primitiveType = primitive.type
                        break
                    end
                end

                local go = type(primitiveType) == "number"
                    and GameObject.CreatePrimitive(primitiveType)
                    or Object.Instantiate(primitiveType)

                go.name = "(" .. x .. "," .. y .. "," .. z .. ")"
                go.transform.parent = rootObject.transform
                go.transform.position = Vector3(x + x * 0.5, y + y * 1, z + z * 0.5)
            end
        end
    end

    -- select the root in the scene
    --Selection.SetActiveObjectWithContext(rootObject, rootObject)
end
]]
