local context = ...
context.Reloads = context.Reloads and context.Reloads + 1 or 1

print("Reloaded script " .. context.Reloads .. " times")


context.OnPostprocessAllAssets = function(importedPaths)
    if #importedPaths == 1 and importedPaths[1]:EndsWith(".unity") then
        EditorSceneManager.OpenScene(importedPaths[1]);
    end
end









context.OnScriptUnload = function()
    print("OnScriptUnload: " .. context.ScriptName)
end
context.OnScriptLoad = function()
    print("OnScriptLoad: " .. context.ScriptName)
end
context.Awake = function()
    print("Awake: " .. context.ScriptName)
end
context.OnEnable = function()
    print("OnEnable: " .. context.ScriptName)
end
context.OnDisable = function()
    print("OnDisable: " .. context.ScriptName)
end
context.OnDestroy = function()
    print("OnDestroy: " .. context.ScriptName)
end
