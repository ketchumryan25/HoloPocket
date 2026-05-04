using UnityEngine;
using Unity.VisualScripting;

[UnitTitle("Determine Range Position (Inclusive)")]
[UnitCategory("Math")]
public class RangePositionInclusiveUnit : Unit
{
    // Inputs
    [DoNotSerialize]
    public ValueInput minInput;
    [DoNotSerialize]
    public ValueInput maxInput;
    [DoNotSerialize]
    public ValueInput currentIndexInput;

    // Output
    [DoNotSerialize]
    public ValueOutput positionOutput;

    protected override void Definition()
    {
        minInput = ValueInput<int>("Min");
        maxInput = ValueInput<int>("Max");
        currentIndexInput = ValueInput<int>("Current Index");
        positionOutput = ValueOutput<int>("Position", GetPosition);
    }

    private int GetPosition(Flow flow)
    {
        int min = flow.GetValue<int>(minInput);
        int max = flow.GetValue<int>(maxInput);
        int current = flow.GetValue<int>(currentIndexInput);

        int rangeSize = max - min + 1;

        // Calculate offset from min
        int offset = current - min;

        // Wrap around, including both min and max
        int wrappedOffset = ((offset % rangeSize) + rangeSize) % rangeSize;

        // Convert to 1-based position
        int position = wrappedOffset + 1;

        return position;
    }
}