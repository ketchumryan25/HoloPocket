using Unity.VisualScripting;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;

[UnitTitle("Extract Digits As Float")]
[UnitCategory("Custom")]
public class ExtractDigitsAsFloat : Unit
{

    // Input: string
    [DoNotSerialize]
    public ValueInput inputString;

    // Output: integer
    [DoNotSerialize]
    public ValueOutput outputFloat;

    protected override void Definition()
    {
        // Define input port
        inputString = ValueInput<string>("String", "");
        // Define output port
        outputFloat = ValueOutput<float>("DigitsAsFloat", ProcessString);
    }

    private float ProcessString(Flow flow)
    {
        string str = flow.GetValue<string>(inputString);
        // Use regex to find numbers, including decimal points
        MatchCollection matches = Regex.Matches(str, @"\d+(\.\d+)?");
        string digitsStr = "";

        foreach (Match match in matches)
        {
            digitsStr += match.Value;
        }

        // Parse as float
        if (float.TryParse(digitsStr, out float result))
        {
            return result;
        }
        return 0f;
    }
}