using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;

[UnitTitle("Increase Count Card Data")]
[UnitCategory("Custom/JSON")]
public class IncreaseCountCardData : Unit
{
    // Inputs
    [DoNotSerialize]
    public ValueInput jsonFilePathInput;

    [DoNotSerialize]
    public ValueInput targetKeyInput; // e.g., "hBP01-001_OSR"

    [DoNotSerialize]
    public ControlInput trigger;

    // Outputs
    [DoNotSerialize]
    public ControlOutput exit;

    [DoNotSerialize]
    public ValueOutput successOutput;

    private bool success;

    protected override void Definition()
    {
        jsonFilePathInput = ValueInput<string>("File Path", "Assets/YourDefaultPath/data.json");
        targetKeyInput = ValueInput<string>("Target Key", "");

        trigger = ControlInput("Trigger", (flow) => {
            Execute(flow);
            return exit;
        });

        exit = ControlOutput("Exit");
        successOutput = ValueOutput<bool>("Success", (flow) => success);

        Succession(trigger, exit);
    }

    private void Execute(Flow flow)
    {
        string filePath = flow.GetValue<string>(jsonFilePathInput);
        string targetKey = flow.GetValue<string>(targetKeyInput);

        success = false;

        if (File.Exists(filePath))
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                var dict = JsonUtility.FromJson<SerializableDictionary>(jsonText);
                var mainDict = dict.ToDictionary();

                // List all top-level keys for debugging
                Debug.Log("Top-level keys in JSON:");
                foreach (var key in mainDict.Keys)
                {
                    Debug.Log($"Key: '{key}'");
                }

                // Check if target key exists
                if (mainDict.ContainsKey(targetKey))
                {
                    var obj = mainDict[targetKey];

                    if (obj is Dictionary<string, object> nestedObj)
                    {
                        if (nestedObj.ContainsKey("count"))
                        {
                            object countVal = nestedObj["count"];

                            if (countVal is long || countVal is int)
                            {
                                int currentCount = Convert.ToInt32(countVal);
                                currentCount += 1;
                                nestedObj["count"] = currentCount;

                                // Save back
                                string updatedJson = JsonUtility.ToJson(SerializableDictionary.FromDictionary(mainDict), true);
                                File.WriteAllText(filePath, updatedJson);
                                success = true;
                            }
                            else
                            {
                                Debug.LogWarning($"'count' in '{targetKey}' is not an integer");
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"'count' key not found in '{targetKey}' object");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"Value for '{targetKey}' is not an object");
                    }
                }
                else
                {
                    Debug.LogWarning($"Target key '{targetKey}' not found");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error processing JSON: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"File not found at path: {filePath}");
        }
    }
}

// Helper class remains the same
[System.Serializable]
public class SerializableDictionary : ISerializationCallbackReceiver
{
    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();

    private Dictionary<string, object> dictionary = new Dictionary<string, object>();

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value != null ? kvp.Value.ToString() : "null");
        }
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<string, object>();
        for (int i = 0; i < keys.Count; i++)
        {
            // Store all as string, cast later if needed
            dictionary[keys[i]] = values[i];
        }
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>(dictionary);
    }

    public static SerializableDictionary FromDictionary(Dictionary<string, object> dict)
    {
        var sdict = new SerializableDictionary();
        sdict.dictionary = new Dictionary<string, object>(dict);
        return sdict;
    }
}