local script = ...

script.OnScriptLoad = function()
    print("(Re-)Load script: " .. script.Name)
end

script.OnPostprocessAllAssets = function(importedPaths)
    if #importedPaths == 1 and importedPaths[1]:EndsWith(".unity") then
        print(script.Name .. " will open created scene: " .. importedPaths[1])
        EditorSceneManager.OpenScene(importedPaths[1]);
    end
end
