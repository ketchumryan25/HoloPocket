using UnityEngine;
using Unity.VisualScripting;

[UnitCategory("String")]
[UnitTitle("Remove All Spaces")]
public class RemoveSpacesUnit : Unit
{
    // Input string
    [DoNotSerialize]
    public ValueInput inputString;

    // Output string
    [DoNotSerialize]
    public ValueOutput outputString;

    protected override void Definition()
    {
        inputString = ValueInput<string>("Input String");
        outputString = ValueOutput<string>("Output String", RemoveSpaces);
        Requirement(inputString, outputString);
    }

    private string RemoveSpaces(Flow flow)
    {
        string str = flow.GetValue<string>(inputString);
        if (str == null)
            return null;
        return str.Replace(" ", "");
    }
}