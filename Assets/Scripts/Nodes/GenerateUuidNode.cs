using UnityEngine;
using Unity.VisualScripting;
using System;

[UnitTitle("Generate UUID")]
[UnitCategory("Custom")]
public class GenerateUuidNode : Unit
{
    // Output: UUID as string
    [DoNotSerialize]
    public ValueOutput uuidOutput;

    protected override void Definition()
    {
        uuidOutput = ValueOutput<string>("UUID", GenerateUuid);
    }

    private string GenerateUuid(Flow flow)
    {
        return Guid.NewGuid().ToString();
    }
}