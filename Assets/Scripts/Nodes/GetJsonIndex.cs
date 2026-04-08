using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Get JSON Index")]
[UnitCategory("JSON")]
public class GetJsonIndex : Unit
{
    // Input: JSON string
    [DoNotSerialize]
    public ValueInput jsonString;

    // Input: property name to find
    [DoNotSerialize]
    public ValueInput propertyName;

    // Output: index of property
    [DoNotSerialize]
    public ValueOutput propertyIndex;

    protected override void Definition()
    {
        jsonString = ValueInput<string>("JSON String", "");
        propertyName = ValueInput<string>("Property Name", "");
        propertyIndex = ValueOutput<int>("Property Index", GetPropertyIndex);
        Requirement(jsonString, propertyIndex);
        Requirement(propertyName, propertyIndex);
    }

    private int GetPropertyIndex(Flow flow)
    {
        string json = flow.GetValue<string>(jsonString);
        string propName = flow.GetValue<string>(propertyName);

        try
        {
            var jObject = JObject.Parse(json);
            int index = 0;
            foreach (var property in jObject.Properties())
            {
                if (property.Name == propName)
                {
                    return index;
                }
                index++;
            }
            // Not found
            return -1;
        }
        catch (System.Exception)
        {
            // Invalid JSON
            return -1;
        }
    }
}