using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

[UnitTitle("Add Nested Property at Index")]
[UnitCategory("JSON")]
public class AddNestedPropertyAtIndex : Unit
{
    // Input: JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Input: Property name to add
    [DoNotSerialize]
    public ValueInput propertyName;

    // Input: Property value
    [DoNotSerialize]
    public ValueInput propertyValue;

    // Input: Index position
    [DoNotSerialize]
    public ValueInput index;

    // Output: Modified JSON string
    [DoNotSerialize]
    public ValueOutput jsonOutput;

    protected override void Definition()
    {
        jsonInput = ValueInput<string>("JSON");
        propertyName = ValueInput<string>("Property Name");
        propertyValue = ValueInput<object>("Property Value");
        index = ValueInput<int>("Index");
        jsonOutput = ValueOutput<string>("Modified JSON", AddPropertyAtIndex);
        Requirement(jsonInput, jsonOutput);
        Requirement(propertyName, jsonOutput);
        Requirement(propertyValue, jsonOutput);
        Requirement(index, jsonOutput);
    }

    private string AddPropertyAtIndex(Flow flow)
    {
        string jsonStr = flow.GetValue<string>(jsonInput);
        string propName = flow.GetValue<string>(propertyName);
        object propVal = flow.GetValue<object>(propertyValue);
        int idx = flow.GetValue<int>(index);

        try
        {
            var jObject = JObject.Parse(jsonStr);

            // Create new property
            var newProperty = new JProperty(propName, JToken.FromObject(propVal));

            // Get list of properties
            var properties = jObject.Properties();

            // Convert to list for index access
            var propList = new List<JProperty>(properties);

            // Clamp index
            if (idx < 0) idx = 0;
            if (idx > propList.Count) idx = propList.Count;

            // Insert at index
            propList.Insert(idx, newProperty);

            // Rebuild JObject
            var newJObject = new JObject();
            foreach (var prop in propList)
            {
                newJObject.Add(prop);
            }

            return newJObject.ToString();
        }
        catch
        {
            // Return original JSON if error occurs
            return jsonStr;
        }
    }
}