using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;

[UnitCategory("Logic")]
[UnitTitle("Is Target Between Min and Max")]
public class IsTargetBetweenUnit : Unit
{
    // Inputs
    [DoNotSerialize]
    public ValueInput min;

    [DoNotSerialize]
    public ValueInput max;

    [DoNotSerialize]
    public ValueInput target;

    // Output
    [DoNotSerialize]
    public ValueOutput isBetween;

    protected override void Definition()
    {
        min = ValueInput<float>("Min");
        max = ValueInput<float>("Max");
        target = ValueInput<float>("Target");
        isBetween = ValueOutput<bool>("Is Between", CheckBetween);

        Requirement(min, isBetween);
        Requirement(max, isBetween);
        Requirement(target, isBetween);
    }

    private bool CheckBetween(Flow flow)
    {
        float minVal = flow.GetValue<float>(min);
        float maxVal = flow.GetValue<float>(max);
        float targetVal = flow.GetValue<float>(target);

        // Check if target is between min and max (inclusive)
        return targetVal >= minVal && targetVal <= maxVal;
    }
}