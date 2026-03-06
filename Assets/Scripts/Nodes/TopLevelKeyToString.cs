using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

[UnitTitle("Top-Level Key by Index")]
[UnitCategory("Custom/JSON")]
public class TopLevelKeyByIndexNode : Unit
{

    // Input for file path
    [DoNotSerialize]
    public ValueInput jsonFilePathInput;

    // Input for index
    [DoNotSerialize]
    public ValueInput indexInput;

    // Output for the key at the given index
    [DoNotSerialize]
    public ValueOutput keyOutput;

    // Control input trigger
    [DoNotSerialize]
    public ControlInput trigger;

    // Control output
    [DoNotSerialize]
    public ControlOutput exit;

    protected override void Definition()
    {
        jsonFilePathInput = ValueInput<string>("File Path", "Assets/YourDefaultPath/data.json");
        indexInput = ValueInput<int>("Index", 0);
        trigger = ControlInput("Trigger", (flow) => {
            flow.SetValue(this.keyOutput, GetKeyAtIndex(flow));
            return exit;
        });
        exit = ControlOutput("Exit");
        keyOutput = ValueOutput<string>("Key at Index");
        Succession(trigger, exit);
    }

    private string GetKeyAtIndex(Flow flow)
    {
        string filePath = flow.GetValue<string>(jsonFilePathInput);
        int index = Mathf.Clamp(flow.GetValue<int>(indexInput), 0, int.MaxValue);
        // Ensure index is non-negative

        if (File.Exists(filePath))
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                var mainDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonText);
                if (mainDict != null)
                {
                    var keysList = new List<string>(mainDict.Keys);
                    if (index >= 0 && index < keysList.Count)
                    {
                        return keysList[index];
                    }
                    else
                    {
                        Debug.LogWarning($"Index {index} out of range. Total keys: {keysList.Count}");
                        return null;
                    }
                }
                else
                {
                    Debug.LogWarning("JSON deserialized to null dictionary.");
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

        return null;
    }
}