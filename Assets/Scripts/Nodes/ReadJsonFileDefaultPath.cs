using UnityEngine;
using Unity.VisualScripting;
using System.IO;
using System;

[UnitTitle("Read JSON from File in Persistent Path")]
[UnitCategory("Custom/File")]
public class ReadJsonFileDefaultPath : Unit
{

    // Input: filename (optional)
    [DoNotSerialize]
    public ValueInput filenameInput;

    // Output: JSON data
    [DoNotSerialize]
    public ValueOutput jsonOutput;

    protected override void Definition()
    {
        filenameInput = ValueInput<string>("Filename", "data.json");
        jsonOutput = ValueOutput<string>("JSON Data", GetJsonFromFile);
    }

    private string GetJsonFromFile(Flow flow)
    {
        string filename = flow.GetValue<string>(filenameInput);
        string path = Path.Combine(Application.persistentDataPath, filename);

        if (!File.Exists(path))
        {
            //Debug.LogWarning($"File not found: {path}");
            return ""; // or null, or a default value
        }

        try
        {
            string jsonData = File.ReadAllText(path);
            //Debug.Log($"Read JSON from: {path}");
            return jsonData;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error reading file: {e.Message}");
            return "";
        }
    }
}