using Unity.VisualScripting;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

[UnitCategory("JSON")]
[UnitTitle("Format JSON")]
public class FormatJsonUnit : Unit
{

    // Input port for JSON string
    [DoNotSerialize]
    public ValueInput jsonInput;

    // Output port for formatted JSON string
    [DoNotSerialize]
    public ValueOutput formattedJsonOutput;

    protected override void Definition()
    {
        // Define input port
        jsonInput = ValueInput<string>("JSON String");
        // Define output port with a delegate to provide the formatted JSON
        formattedJsonOutput = ValueOutput<string>("Formatted JSON", FormatJson);
        // Declare that the output depends on the input
        Requirement(jsonInput, formattedJsonOutput);
    }

    private string FormatJson(Flow flow)
    {
        string jsonString = flow.GetValue<string>(jsonInput);
        if (string.IsNullOrEmpty(jsonString))
        {
            return string.Empty;
        }
        try
        {
            // Parse the JSON string and then serialize it with indentation
            var parsedJson = JsonConvert.DeserializeObject(jsonString);
            string formattedJson = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            return formattedJson;
        }
        catch (JsonException)
        {
            // If parsing fails, return the original string or handle as needed
            return jsonString;
        }
    }
}