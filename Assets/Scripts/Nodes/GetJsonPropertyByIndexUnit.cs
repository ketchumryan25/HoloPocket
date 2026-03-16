using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

[UnitCategory("JSON")]
[UnitTitle("Get JSON Property by Index")]
public class GetJsonPropertyByIndexUnit : Unit
{
    // Input: JSON object string
    [DoNotSerialize]
    public ValueInput jsonObjectString;

    // Input: Index of the property
    [DoNotSerialize]
    public ValueInput index;

    // Output: The property value as JSON string
    [DoNotSerialize]
    public ValueOutput propertyValue;

    protected override void Definition()
    {
        jsonObjectString = ValueInput<string>("JSON Object");
        index = ValueInput<int>("Index");
        propertyValue = ValueOutput<string>("Property Value", GetPropertyByIndex);

        Requirement(jsonObjectString, propertyValue);
        Requirement(index, propertyValue);
    }

    private string GetPropertyByIndex(Flow flow)
    {
        string jsonStr = flow.GetValue<string>(jsonObjectString);
        int idx = flow.GetValue<int>(index);

        try
        {
            var jObject = JObject.Parse(jsonStr);
            var properties = jObject.Properties().ToList();

            if (properties.Count > idx && idx >= 0)
            {
                var prop = properties[idx];
                // Return the property's value as a JSON string
                return prop.Value.ToString(Formatting.None);
            }
            else
            {
                Debug.LogWarning("Index out of range");
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return null;
        }
    }
}