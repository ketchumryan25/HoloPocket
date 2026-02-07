using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;

[UnitTitle("Random Unique Select Strings")]
[UnitCategory("Data")]
public class RandomUniqueSelectStringsUnit : Unit
{
    // Input: List of strings
    [DoNotSerialize]
    public ValueInput stringListInput;

    // Input: Number of strings to select
    [DoNotSerialize]
    public ValueInput countInput;

    // Output: List of randomly selected unique strings
    [DoNotSerialize]
    public ValueOutput outputList;

    protected override void Definition()
    {
        stringListInput = ValueInput<List<string>>("String List");
        countInput = ValueInput<int>("Number to Select");
        outputList = ValueOutput<List<string>>("Result List", GenerateUniqueRandomSelection);
    }

    private List<string> GenerateUniqueRandomSelection(Flow flow)
    {
        List<string> inputList = flow.GetValue<List<string>>(stringListInput);
        int count = flow.GetValue<int>(countInput);
        List<string> result = new List<string>();

        if (inputList == null || inputList.Count == 0 || count <= 0)
            return result;

        // Clamp count to the size of the input list
        count = Mathf.Min(count, inputList.Count);

        // Make a copy of the list to shuffle
        List<string> tempList = new List<string>(inputList);

        // Shuffle the list
        for (int i = 0; i < tempList.Count; i++)
        {
            int randIndex = Random.Range(i, tempList.Count);
            // Swap
            string temp = tempList[i];
            tempList[i] = tempList[randIndex];
            tempList[randIndex] = temp;
        }

        // Take the first 'count' items
        for (int i = 0; i < count; i++)
        {
            result.Add(tempList[i]);
        }

        return result;
    }
}