using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Count Nested Objects in JSON")]
[UnitCategory("Custom")]
public class CountNestedObjectsNode : Unit
{
    // Input port for JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Output port for the count
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
        string jsonData = flow.GetValue<string>(jsonInput);

        if (string.IsNullOrEmpty(jsonData))
            return 0;

        try
        {
            var jToken = JToken.Parse(jsonData);
            return CountObjectsRecursive(jToken);
        }
        catch
        {
            // Invalid JSON
            return 0;
        }
    }

    private int CountObjectsRecursive(JToken token)
    {
        int count = 0;

        if (token.Type == JTokenType.Object)
        {
            count++; // Count this object

            // Recursively count nested objects
            foreach (var child in token.Children<JProperty>())
            {
                count += CountObjectsRecursive(child.Value);
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            // Optionally, count objects inside arrays if needed
            foreach (var item in token.Children())
            {
                count += CountObjectsRecursive(item);
            }
        }

        return count;
    }
}