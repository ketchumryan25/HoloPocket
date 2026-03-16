using Unity.VisualScripting;
using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;


[UnitCategory("File")]
[UnitTitle("Copy JSON File to Persistent Data Path")]
public class CopyJsonFile : Unit
{

    // Input port for source file path
    [DoNotSerialize]
    public ValueInput sourceFilePath;

    // Optional input for destination filename
    [DoNotSerialize]
    public ValueInput destinationFileName;

    // Output port indicating success or failure
    [DoNotSerialize]
    public ValueOutput success;

    protected override void Definition()
    {
        sourceFilePath = ValueInput<string>("Source File Path");
        destinationFileName = ValueInput<string>("Destination File Name", "copied.json");
        success = ValueOutput<bool>("Success", CopyFile);
        Requirement(sourceFilePath, success);
        Requirement(destinationFileName, success);
    }

    private bool CopyFile(Flow flow)
    {
        string sourcePath = flow.GetValue<string>(sourceFilePath);
        string destFileName = flow.GetValue<string>(destinationFileName);
        string destPath = Path.Combine(Application.persistentDataPath, destFileName);

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destPath, overwrite: true);
                return true; // Copy successful
            }
            else
            {
                Debug.LogWarning($"Source file not found: {sourcePath}");
                return false; // Source file not found
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error copying file: {e.Message}");
            return false;
        }
    }
}