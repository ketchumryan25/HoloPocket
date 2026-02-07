using UnityEngine;
using Unity.VisualScripting;
using System.IO;

// Define a custom unit for JSON writing with multiple string inputs and filename
[UnitTitle("Write Perk Data to JSON")]
[UnitCategory("Custom")]
public class WritePerkDataToJsonUnit : Unit
{
    // Inputs
    [DoNotSerialize]
    public ControlInput inputTrigger;

    // Outputs
    [DoNotSerialize]
    public ControlOutput outputTrigger;

    // String data inputs for each perk
    [DoNotSerialize]
    public ValueInput column1Perk1;
    [DoNotSerialize]
    public ValueInput column1Perk2;
    [DoNotSerialize]
    public ValueInput column2Perk1;
    [DoNotSerialize]
    public ValueInput column2Perk2;
    [DoNotSerialize]
    public ValueInput column3Perk1;
    [DoNotSerialize]
    public ValueInput column3Perk2;
    [DoNotSerialize]
    public ValueInput column4Perk1;
    [DoNotSerialize]
    public ValueInput column4Perk2;
    [DoNotSerialize]
    public ValueInput column5Perk1;
    [DoNotSerialize]
    public ValueInput column5Perk2;

    // Filename input
    [DoNotSerialize]
    public ValueInput filenameInput;

    protected override void Definition()
    {
        inputTrigger = ControlInput("In", WriteData);
        outputTrigger = ControlOutput("Out");

        // Define string inputs for each perk
        column1Perk1 = ValueInput<string>("Column 1 Perk 1", "");
        column1Perk2 = ValueInput<string>("Column 1 Perk 2", "");
        column2Perk1 = ValueInput<string>("Column 2 Perk 1", "");
        column2Perk2 = ValueInput<string>("Column 2 Perk 2", "");
        column3Perk1 = ValueInput<string>("Column 3 Perk 1", "");
        column3Perk2 = ValueInput<string>("Column 3 Perk 2", "");
        column4Perk1 = ValueInput<string>("Column 4 Perk 1", "");
        column4Perk2 = ValueInput<string>("Column 4 Perk 2", "");
        column5Perk1 = ValueInput<string>("Column 5 Perk 1", "");
        column5Perk2 = ValueInput<string>("Column 5 Perk 2", "");

        // Filename input
        filenameInput = ValueInput<string>("File Name", "perkData.json");

        // Set the execution order
        Succession(inputTrigger, outputTrigger);
    }

    private ControlOutput WriteData(Flow flow)
    {
        // Gather all string inputs
        var data = new PerkData
        {
            column1Perk1 = flow.GetValue<string>(column1Perk1),
            column1Perk2 = flow.GetValue<string>(column1Perk2),
            column2Perk1 = flow.GetValue<string>(column2Perk1),
            column2Perk2 = flow.GetValue<string>(column2Perk2),
            column3Perk1 = flow.GetValue<string>(column3Perk1),
            column3Perk2 = flow.GetValue<string>(column3Perk2),
            column4Perk1 = flow.GetValue<string>(column4Perk1),
            column4Perk2 = flow.GetValue<string>(column4Perk2),
            column5Perk1 = flow.GetValue<string>(column5Perk1),
            column5Perk2 = flow.GetValue<string>(column5Perk2),
        };

        // Get filename and ensure it ends with .json
        string filename = flow.GetValue<string>(filenameInput);
        if (string.IsNullOrEmpty(filename))
        {
            Debug.LogWarning("Filename is empty. Using default 'perkData.json'.");
            filename = "perkData.json";
        }
        else if (!filename.EndsWith(".json"))
        {
            filename += ".json";
        }

        string filePath = Path.Combine(Application.persistentDataPath, filename);

        // Serialize to JSON
        string json = JsonUtility.ToJson(data, true);

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("Perk data saved to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to write file: " + e.Message);
        }

        return outputTrigger;
    }

    [System.Serializable]
    public class PerkData
    {
        public string column1Perk1;
        public string column1Perk2;
        public string column2Perk1;
        public string column2Perk2;
        public string column3Perk1;
        public string column3Perk2;
        public string column4Perk1;
        public string column4Perk2;
        public string column5Perk1;
        public string column5Perk2;
    }
}