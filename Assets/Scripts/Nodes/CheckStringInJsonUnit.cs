using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;

[UnitTitle("Check String in JSON")]
[UnitCategory("Data")]
public class CheckStringInJsonUnit : Unit
{
    // Input: JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Input: String to compare
    [DoNotSerialize]
    public ValueInput compareStringInput;

    // Output: Boolean result
    [DoNotSerialize]
    public ValueOutput resultOutput;

    protected override void Definition()
    {
        jsonInput = ValueInput<string>("JSON");
        compareStringInput = ValueInput<string>("Compare String");
        resultOutput = ValueOutput<bool>("Found Match", CheckForMatch);

        Requirement(jsonInput, resultOutput);
        Requirement(compareStringInput, resultOutput);
    }

    private bool CheckForMatch(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonInput);
        string compareStr = flow.GetValue<string>(compareStringInput);

        if (string.IsNullOrEmpty(jsonString) || string.IsNullOrEmpty(compareStr))
            return false;

        try
        {
            var token = JToken.Parse(jsonString);
            return SearchToken(token, compareStr);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"JSON parsing error: {e.Message}");
            return false;
        }
    }

    private bool SearchToken(JToken token, string compareStr)
    {
        if (token.Type == JTokenType.String)
        {
            if (token.Value<string>() == compareStr)
                return true;
        }
        else if (token.Type == JTokenType.Object)
        {
            foreach (var property in token.Children<JProperty>())
            {
                if (SearchToken(property.Value, compareStr))
                    return true;
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            foreach (var item in token.Children())
            {
                if (SearchToken(item, compareStr))
                    return true;
            }
        }
        return false;
    }
}