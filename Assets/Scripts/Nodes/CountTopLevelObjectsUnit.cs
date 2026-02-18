using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;
using System.Linq; // Make sure Newtonsoft.Json package is included

[UnitTitle("Count Top-Level JSON Objects")]
[UnitCategory("Data")]
public class CountTopLevelObjectsUnit : Unit
{
    // Input: JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Output: Number of top-level objects
    [DoNotSerialize]
    public ValueOutput countOutput;

    protected override void Definition()
    {
        jsonInput = ValueInput<string>("JSON");
        countOutput = ValueOutput<int>("Count", CountObjects);
        Requirement(jsonInput, countOutput);
    }

    private int CountObjects(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonInput);
        if (string.IsNullOrEmpty(jsonString))
            return 0;

        try
        {
            var token = JToken.Parse(jsonString);
            if (token.Type == JTokenType.Object)
            {
                var obj = (JObject)token;
                // Count the number of properties (key-value pairs) at the first level
                return obj.Properties().Count();
            }
            else
            {
                // Not a JSON object at top level
                return 0;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return 0;
        }
    }
}