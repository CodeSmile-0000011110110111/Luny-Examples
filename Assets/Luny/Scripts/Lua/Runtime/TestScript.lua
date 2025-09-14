local script = ...

-- since we don't set this value in Inspector, we have to initialize it to zero
-- otherwise the value would be 'nil' and cannot do math with 'nil' values
script.currentSpeed = 0

script.Update = function()
    -- as it was before, except we're now using script.currentSpeed
    local x = script.transform.position.x
    script.transform.position = Vector3(x + script.currentSpeed * Time.deltaTime, 0, 0)

    -- let's make the object stop if it's already moving very slowly
    -- this avoids currentSpeed printing long scientific notation numbers in the Inspector
    if Mathf.Abs(script.currentSpeed) < 0.001 then
        script.currentSpeed = 0
    end

    -- if the 'push' checkbox is checked, we update currentSpeed to the speed value
    if script.push then
        script.push = false
        script.currentSpeed = script.speed
    end

    -- ensure the factor doesn't increase speed (risks errors like 'Object too large or too far away')
    script.slowdownFactor = Mathf.Clamp(script.slowdownFactor, -0.99, 0.99)
    script.currentSpeed = script.currentSpeed * script.slowdownFactor
end
