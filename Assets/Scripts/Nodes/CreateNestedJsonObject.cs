using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Create Nested JSON Object")]
[UnitCategory("JSON")]
public class CreateNestedJsonObject : Unit
{
    // Input: Key
    [DoNotSerialize]
    public ValueInput keyInput;

    // Input: Value
    [DoNotSerialize]
    public ValueInput valueInput;

    // Output: Nested JSON string
    [DoNotSerialize]
    public ValueOutput jsonOutput;

    protected override void Definition()
    {
        keyInput = ValueInput<string>("Key");
        valueInput = ValueInput<object>("Value");
        jsonOutput = ValueOutput<string>("JSON", CreateNestedJson);
        Requirement(keyInput, jsonOutput);
        Requirement(valueInput, jsonOutput);
    }

    private string CreateNestedJson(Flow flow)
    {
        string key = flow.GetValue<string>(keyInput);
        object value = flow.GetValue<object>(valueInput);

        // Wrap the key-value pair inside another object for nesting
        var nestedObject = new JObject
        {
            [key] = JToken.FromObject(value)
        };

        return nestedObject.ToString();
    }
}