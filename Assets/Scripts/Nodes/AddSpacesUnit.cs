using UnityEngine;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

[UnitCategory("String")]
[UnitTitle("Add Spaces")]
public class AddSpacesUnit : Unit
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
        outputString = ValueOutput<string>("Output String", AddSpaces);
        Requirement(inputString, outputString);
    }

    private string AddSpaces(Flow flow)
    {
        string str = flow.GetValue<string>(inputString);
        if (string.IsNullOrEmpty(str))
            return str;

        // Insert spaces before uppercase letters (e.g., "HelloWorld" -> "Hello World")
        string result = Regex.Replace(str, "(?<!^)([A-Z])", " $1");
        // Insert spaces before digits (e.g., "abc123" -> "abc 123")
        result = Regex.Replace(result, "(?<=\\D)(\\d)", " $1");

        // Optional: handle underscores or other delimiters
        result = result.Replace("_", " ");

        return result;
    }
}