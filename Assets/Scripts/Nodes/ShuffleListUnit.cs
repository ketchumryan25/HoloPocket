using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;

[UnitTitle("Shuffle List")]
[UnitCategory("List")]
public class ShuffleListUnit : Unit
{
    // Input: List of any type
    [DoNotSerialize]
    public ValueInput listInput;

    // Output: Shuffled list (same type as input)
    [DoNotSerialize]
    public ValueOutput shuffledList;

    protected override void Definition()
    {
        listInput = ValueInput<IList>("List");
        shuffledList = ValueOutput<IList>("Shuffled List", Shuffle);
        Requirement(listInput, shuffledList);
    }

    private IList Shuffle(Flow flow)
    {
        // Get the original list
        var originalList = flow.GetValue<IList>(listInput);
        // Create a copy of the list
        var listCopy = new ArrayList(originalList);

        int n = listCopy.Count;
        for (int i = 0; i < n; i++)
        {
            int randIndex = Random.Range(i, n);
            var temp = listCopy[i];
            listCopy[i] = listCopy[randIndex];
            listCopy[randIndex] = temp;
        }

        // Convert back to IList of the same type
        return listCopy;
    }
}