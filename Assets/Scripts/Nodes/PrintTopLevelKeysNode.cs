using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json; // Make sure Newtonsoft.Json is imported into your project

[UnitTitle("Print Top-Level Keys in JSON (Newtonsoft)")]
[UnitCategory("Custom/JSON")]
public class PrintTopLevelKeysNode : Unit
{
    // Input for file path
    [DoNotSerialize]
    public ValueInput jsonFilePathInput;

    // Control input trigger
    [DoNotSerialize]
    public ControlInput trigger;

    // Control output
    [DoNotSerialize]
    public ControlOutput exit;

    protected override void Definition()
    {
        jsonFilePathInput = ValueInput<string>("File Path", "Assets/YourDefaultPath/data.json");
        trigger = ControlInput("Trigger", (flow) => {
            PrintKeys(flow);
            return exit;
        });
        exit = ControlOutput("Exit");
        Succession(trigger, exit);
    }

    private void PrintKeys(Flow flow)
    {
        string filePath = flow.GetValue<string>(jsonFilePathInput);

        if (File.Exists(filePath))
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                // Deserialize into a Dictionary<string, object> using Newtonsoft.Json
                var mainDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);

                Debug.Log("Top-level keys in JSON:");
                foreach (var key in mainDict.Keys)
                {
                    Debug.Log($"Key: '{key}'");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading JSON with Newtonsoft.Json: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"File not found at path: {filePath}");
        }
    }
}