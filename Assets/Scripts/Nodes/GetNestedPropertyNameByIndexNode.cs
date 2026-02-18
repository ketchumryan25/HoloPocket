using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Get Nested Property Name by Index")]
[UnitCategory("Custom")]
public class GetNestedPropertyNameByIndexNode : Unit
{
    // Input port for JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Input port for index
    [DoNotSerialize]
    public ValueInput indexInput;

    // Output port for property name
    [DoNotSerialize]
    public ValueOutput propertyNameOutput;

    protected override void Definition()
    {
        jsonInput = ValueInput<string>("JSON");
        indexInput = ValueInput<int>("Index");
        propertyNameOutput = ValueOutput<string>("PropertyName", GetPropertyName);

        Requirement(jsonInput, propertyNameOutput);
        Requirement(indexInput, propertyNameOutput);
    }

    private string GetPropertyName(Flow flow)
    {
        string jsonData = flow.GetValue<string>(jsonInput);
        int index = flow.GetValue<int>(indexInput);

        if (string.IsNullOrEmpty(jsonData))
            return "Invalid JSON";

        try
        {
            // Parse JSON data
            var jToken = JToken.Parse(jsonData);

            // Get all property names at the current level
            var propertyNames = new System.Collections.Generic.List<string>();
            if (jToken.Type == JTokenType.Object)
            {
                foreach (var prop in jToken.Children<JProperty>())
                {
                    propertyNames.Add(prop.Name);
                }
            }
            else
            {
                return "JSON is not an object";
            }

            // Check if index is within range
            if (index < 0 || index >= propertyNames.Count)
                return "Index out of range";

            // Return the property name at the specified index
            return propertyNames[index];
        }
        catch
        {
            return "Invalid JSON format";
        }
    }
}