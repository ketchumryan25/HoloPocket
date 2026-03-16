using Unity.VisualScripting;
using UnityEngine;
using System.IO;

[UnitCategory("File")]
[UnitTitle("Does File Exist")]
public class DoesFileExistUnit : Unit
{

    // Input port for filename
    [DoNotSerialize]
    public ValueInput fileNameInput;

    // Output port for existence check
    [DoNotSerialize]
    public ValueOutput fileExistsOutput;

    protected override void Definition()
    {
        fileNameInput = ValueInput<string>("File Name");
        fileExistsOutput = ValueOutput<bool>("File Exists", CheckFileExists);
        Requirement(fileNameInput, fileExistsOutput);
    }

    private bool CheckFileExists(Flow flow)
    {
        string fileName = flow.GetValue<string>(fileNameInput);
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        return File.Exists(filePath);
    }
}