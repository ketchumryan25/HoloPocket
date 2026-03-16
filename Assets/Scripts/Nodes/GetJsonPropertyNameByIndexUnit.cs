using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

[UnitCategory("JSON")]
[UnitTitle("Get JSON Property Name by Index")]
public class GetJsonPropertyNameByIndexUnit : Unit
{
    // Input: JSON object string
    [DoNotSerialize]
    public ValueInput jsonObjectString;

    // Input: Index of the property
    [DoNotSerialize]
    public ValueInput index;

    // Output: The property name at the specified index
    [DoNotSerialize]
    public ValueOutput propertyName;

    protected override void Definition()
    {
        jsonObjectString = ValueInput<string>("JSON Object");
        index = ValueInput<int>("Index");
        propertyName = ValueOutput<string>("Property Name", GetPropertyNameByIndex);

        Requirement(jsonObjectString, propertyName);
        Requirement(index, propertyName);
    }

    private string GetPropertyNameByIndex(Flow flow)
    {
        string jsonStr = flow.GetValue<string>(jsonObjectString);
        int idx = flow.GetValue<int>(index);

        try
        {
            var jObject = JObject.Parse(jsonStr);
            var properties = jObject.Properties().ToList();

            if (properties.Count > idx && idx >= 0)
            {
                return properties[idx].Name;
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