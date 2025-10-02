local script = ...

--script.OnPostprocessAllAssets = function(importedPaths)
--    if #importedPaths == 1 and importedPaths[1]:EndsWith(".unity") then
--        print("active scene: " .. tostring(EditorSceneManager.GetActiveScene()))
--        if EditorSceneManager.GetActiveScene().path ~= importedPaths[1] then
--            print(script.Name .. " will open created scene: " .. importedPaths[1])
--            EditorSceneManager.OpenScene(importedPaths[1]);
--        end
--    end
--end
