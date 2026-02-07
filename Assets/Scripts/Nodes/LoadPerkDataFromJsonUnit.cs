using UnityEngine;
using Unity.VisualScripting;
using System.IO;

[UnitTitle("Load Perk Data from JSON")]
[UnitCategory("Custom")]
public class LoadPerkDataFromJsonUnit : Unit
{
    // Control flow inputs and outputs
    private ControlInput inputTrigger;
    private ControlOutput outputTrigger;

    // Filename input
    private ValueInput filenameInput;

    // Perk data outputs
    private ValueOutput column1Perk1;
    private ValueOutput column1Perk2;
    private ValueOutput column2Perk1;
    private ValueOutput column2Perk2;
    private ValueOutput column3Perk1;
    private ValueOutput column3Perk2;
    private ValueOutput column4Perk1;
    private ValueOutput column4Perk2;
    private ValueOutput column5Perk1;
    private ValueOutput column5Perk2;

    protected override void Definition()
    {
        // Define control flow connection
        inputTrigger = ControlInput("In", LoadData);
        outputTrigger = ControlOutput("Out");

        // Define filename input
        filenameInput = ValueInput<string>("File Name", "perkData.json");

        // Define output variables
        column1Perk1 = ValueOutput<string>("Column 1 Perk 1");
        column1Perk2 = ValueOutput<string>("Column 1 Perk 2");
        column2Perk1 = ValueOutput<string>("Column 2 Perk 1");
        column2Perk2 = ValueOutput<string>("Column 2 Perk 2");
        column3Perk1 = ValueOutput<string>("Column 3 Perk 1");
        column3Perk2 = ValueOutput<string>("Column 3 Perk 2");
        column4Perk1 = ValueOutput<string>("Column 4 Perk 1");
        column4Perk2 = ValueOutput<string>("Column 4 Perk 2");
        column5Perk1 = ValueOutput<string>("Column 5 Perk 1");
        column5Perk2 = ValueOutput<string>("Column 5 Perk 2");

        // Set up flow connection
        Succession(inputTrigger, outputTrigger);
    }

    private ControlOutput LoadData(Flow flow)
    {
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

        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            // Set empty strings if file doesn't exist
            flow.SetValue(column1Perk1, "");
            flow.SetValue(column1Perk2, "");
            flow.SetValue(column2Perk1, "");
            flow.SetValue(column2Perk2, "");
            flow.SetValue(column3Perk1, "");
            flow.SetValue(column3Perk2, "");
            flow.SetValue(column4Perk1, "");
            flow.SetValue(column4Perk2, "");
            flow.SetValue(column5Perk1, "");
            flow.SetValue(column5Perk2, "");
            return null; // Exit early
        }

        try
        {
            string json = File.ReadAllText(filePath);
            var data = JsonUtility.FromJson<PerkData>(json);

            // Set the variables
            flow.SetValue(column1Perk1, data.column1Perk1);
            flow.SetValue(column1Perk2, data.column1Perk2);
            flow.SetValue(column2Perk1, data.column2Perk1);
            flow.SetValue(column2Perk2, data.column2Perk2);
            flow.SetValue(column3Perk1, data.column3Perk1);
            flow.SetValue(column3Perk2, data.column3Perk2);
            flow.SetValue(column4Perk1, data.column4Perk1);
            flow.SetValue(column4Perk2, data.column4Perk2);
            flow.SetValue(column5Perk1, data.column5Perk1);
            flow.SetValue(column5Perk2, data.column5Perk2);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading or parsing JSON: " + e.Message);
            // Set empty strings on error
            flow.SetValue(column1Perk1, "");
            flow.SetValue(column1Perk2, "");
            flow.SetValue(column2Perk1, "");
            flow.SetValue(column2Perk2, "");
            flow.SetValue(column3Perk1, "");
            flow.SetValue(column3Perk2, "");
            flow.SetValue(column4Perk1, "");
            flow.SetValue(column4Perk2, "");
            flow.SetValue(column5Perk1, "");
            flow.SetValue(column5Perk2, "");
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