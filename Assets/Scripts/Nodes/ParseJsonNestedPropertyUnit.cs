using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Parse JSON Nested Property")]
[UnitCategory("Data")]
public class ParseJsonNestedPropertyUnit : Unit
{
    // Input: JSON string
    [DoNotSerialize]
    public ValueInput jsonStringInput;

    // Input: Key path for nested property (e.g., "player.health" or "data.items[0].name")
    [DoNotSerialize]
    public ValueInput keyPathInput;

    // Output: JSON string of the nested object/property
    [DoNotSerialize]
    public ValueOutput outputJson;

    // Output: Whether the nested property is a boolean
    [DoNotSerialize]
    public ValueOutput isBooleanOutput;

    // Output: The boolean value of the property (true/false), default false if not found or not boolean
    [DoNotSerialize]
    public ValueOutput booleanValueOutput;

    protected override void Definition()
    {
        jsonStringInput = ValueInput<string>("JSON String");
        keyPathInput = ValueInput<string>("Key Path");
        outputJson = ValueOutput<string>("Nested JSON", GetNestedJson);
        isBooleanOutput = ValueOutput<bool>("Is Boolean", CheckIfBoolean);
        booleanValueOutput = ValueOutput<bool>("Boolean Value", GetBooleanValue);

        Requirement(jsonStringInput, outputJson);
        Requirement(keyPathInput, outputJson);
        Requirement(jsonStringInput, isBooleanOutput);
        Requirement(keyPathInput, isBooleanOutput);
        Requirement(jsonStringInput, booleanValueOutput);
        Requirement(keyPathInput, booleanValueOutput);
    }

    private string GetNestedJson(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonStringInput);
        string keyPath = flow.GetValue<string>(keyPathInput);

        if (string.IsNullOrEmpty(jsonString) || string.IsNullOrEmpty(keyPath))
            return null;

        try
        {
            var jsonObject = JObject.Parse(jsonString);
            JToken token = jsonObject.SelectToken(keyPath);
            if (token != null)
            {
                return token.ToString();
            }
            else
            {
                return null; // Key path not found
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return null;
        }
    }

    private bool CheckIfBoolean(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonStringInput);
        string keyPath = flow.GetValue<string>(keyPathInput);

        if (string.IsNullOrEmpty(jsonString) || string.IsNullOrEmpty(keyPath))
            return false;

        try
        {
            var jsonObject = JObject.Parse(jsonString);
            JToken token = jsonObject.SelectToken(keyPath);
            if (token != null && token.Type == JTokenType.Boolean)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return false;
        }
    }

    private bool GetBooleanValue(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonStringInput);
        string keyPath = flow.GetValue<string>(keyPathInput);

        if (string.IsNullOrEmpty(jsonString) || string.IsNullOrEmpty(keyPath))
            return false;

        try
        {
            var jsonObject = JObject.Parse(jsonString);
            JToken token = jsonObject.SelectToken(keyPath);
            if (token != null && token.Type == JTokenType.Boolean)
            {
                return token.Value<bool>();
            }
            else
            {
                return false; // Not found or not boolean
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return false;
        }
    }
}