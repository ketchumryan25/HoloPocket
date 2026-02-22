using UnityEngine;
using Unity.VisualScripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[UnitTitle("Card Data to Json Data")]
[UnitCategory("Custom")]
public class CardDataToJsonData : Unit
{
    // Control ports
    public ControlInput inputTrigger;
    public ControlOutput outputTrigger;

    // Data ports
    public ValueOutput jsonOutput;

    // Data inputs
    public ValueInput cardName;
    public ValueInput sourceName;
    public ValueInput cardIndex;
    public ValueInput sourceIndex;
    public ValueInput rarity;
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
        cardName = ValueInput<string>("Card Name", "");
        sourceName = ValueInput<string>("Card Source", "");
        cardIndex = ValueInput<string>("Card Index", "");
        sourceIndex = ValueInput<string>("Source Index", "");
        rarity = ValueInput<string>("Card Rarity", "");
        count = ValueInput<int>("Card Amount", 0);
        nestedObjectName = ValueInput<string>("Nested Object Name", "Source Index");
    }

    private ControlOutput GenerateJson(Flow flow)
    {
        // Gather data
        var data = new CardData
        {
            cardName = flow.GetValue<string>(cardName),
            sourceName = flow.GetValue<string>(sourceName),
            cardIndex = flow.GetValue<string>(cardIndex),
            sourceIndex = flow.GetValue<string>(sourceIndex),
            rarity = flow.GetValue<string>(rarity),
            count = flow.GetValue<int>(count),
        };

        // Get nested object name
        string nestedName = flow.GetValue<string>(nestedObjectName);
        if (string.IsNullOrEmpty(nestedName))
            nestedName = "Source Index";

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
        public string cardName;
        public string sourceName;
        public string cardIndex;
        public string sourceIndex;
        public string rarity;
        public int count;
    }
}