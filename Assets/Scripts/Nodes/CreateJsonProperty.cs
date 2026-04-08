using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Create JSON Property")]
[UnitCategory("JSON")]
public class CreateJsonProperty : Unit
{
    // Input: Property key
    [DoNotSerialize]
    public ValueInput keyInput;

    // Input: Property value
    [DoNotSerialize]
    public ValueInput valueInput;

    // Output: JSON property string
    [DoNotSerialize]
    public ValueOutput propertyOutput;

    protected override void Definition()
    {
        keyInput = ValueInput<string>("Key");
        valueInput = ValueInput<object>("Value");
        propertyOutput = ValueOutput<string>("JSON Property", CreateProperty);
        Requirement(keyInput, propertyOutput);
        Requirement(valueInput, propertyOutput);
    }

    private string CreateProperty(Flow flow)
    {
        string key = flow.GetValue<string>(keyInput);
        object value = flow.GetValue<object>(valueInput);

        // Create a JObject with one property
        var jObject = new JObject
        {
            [key] = JToken.FromObject(value)
        };

        // Serialize to JSON string
        return jObject.ToString();
    }
}