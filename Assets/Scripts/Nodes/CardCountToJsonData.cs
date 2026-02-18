using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[UnitTitle("Card Count to Json Data")]
[UnitCategory("Custom")]
public class CardCountToJsonData : Unit
{
    // Control ports
    public ControlInput inputTrigger;
    public ControlOutput outputTrigger;

    // Data ports
    public ValueOutput jsonOutput;

    // Data inputs
    public ValueInput count;
    public ValueInput nestedObjectName;

    protected override void Definition()
    {
        // Define control flow ports
        inputTrigger = ControlInput("In", GenerateJson);
        outputTrigger = ControlOutput("Out");
        Succession(inputTrigger, outputTrigger);

        // Define value output
        jsonOutput = ValueOutput<string>("JSON Output");

        // Define value inputs
        count = ValueInput<int>("Card Amount", 0);
        nestedObjectName = ValueInput<string>("Nested Name", "Full Card Index");
    }

    private ControlOutput GenerateJson(Flow flow)
    {
        // Gather data
        var data = new CardData
        {
            count = flow.GetValue<int>(count),
        };

        // Get nested object name
        string nestedName = flow.GetValue<string>(nestedObjectName);
        if (string.IsNullOrEmpty(nestedName))
            nestedName = "Full Card Index";

        // Build nested JSON object
        var root = new Dictionary<string, object>();
        root[nestedName] = data;

        // Serialize to JSON string with indentation
        string jsonString = JsonConvert.SerializeObject(root, Formatting.Indented);

        // Set output value
        flow.SetValue(jsonOutput, jsonString);

        return outputTrigger;
    }

    [System.Serializable]
    public class CardData
    {
        public int count;
    }
}