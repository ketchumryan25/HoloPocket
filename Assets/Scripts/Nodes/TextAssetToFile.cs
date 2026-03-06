using Unity.VisualScripting;
using UnityEngine;
using System.IO;

[UnitCategory("File")]
[UnitTitle("Copy TextAsset to Persistent Data Path")]
public class TextAssetToFile : Unit
{

    // Input port for the TextAsset
    [DoNotSerialize]
    public ValueInput textAssetInput;

    // Optional input for the destination filename
    [DoNotSerialize]
    public ValueInput destinationFileName;

    // Output port indicating success or failure
    [DoNotSerialize]
    public ValueOutput success;

    protected override void Definition()
    {
        textAssetInput = ValueInput<TextAsset>("TextAsset");
        destinationFileName = ValueInput<string>("Destination File Name", "copied.json");
        success = ValueOutput<bool>("Success", CopyTextAsset);
        Requirement(textAssetInput, success);
        Requirement(destinationFileName, success);
    }

    private bool CopyTextAsset(Flow flow)
    {
        var textAsset = flow.GetValue<TextAsset>(textAssetInput);
        string destFileName = flow.GetValue<string>(destinationFileName);

        if (textAsset == null)
        {
            Debug.LogWarning("TextAsset is null");
            return false;
        }

        string destPath = Path.Combine(Application.persistentDataPath, destFileName);

        try
        {
            File.WriteAllText(destPath, textAsset.text);
            Debug.Log($"Copied TextAsset to: {destPath}");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error writing TextAsset: {e.Message}");
            return false;
        }
    }
}