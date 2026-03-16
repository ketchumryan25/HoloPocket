using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

[UnitTitle("Top-Level Keys to List")]
[UnitCategory("Custom/JSON")]
public class TopLevelKeysToList : Unit
{

    // Input for file path
    [DoNotSerialize]
    public ValueInput jsonFilePathInput;

    // Output for list of keys
    [DoNotSerialize]
    public ValueOutput keysOutput;

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
            flow.SetValue(this.keysOutput, GetKeys(flow));
            return exit;
        });
        exit = ControlOutput("Exit");
        keysOutput = ValueOutput<List<string>>("Top-Level Keys");
        Succession(trigger, exit);
    }

    private List<string> GetKeys(Flow flow)
    {
        string filePath = flow.GetValue<string>(jsonFilePathInput);
        var keysList = new List<string>();

        if (File.Exists(filePath))
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                var mainDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);
                if (mainDict != null)
                {
                    keysList.AddRange(mainDict.Keys);
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

        return keysList;
    }
}