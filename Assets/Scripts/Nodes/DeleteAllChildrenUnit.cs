using UnityEngine;
using Unity.VisualScripting;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

[UnitCategory("GameObject")]
[UnitTitle("Delete All Children")]
public class DeleteAllChildrenUnit : Unit
{
    // Input: GameObject whose children will be deleted
    [DoNotSerialize]
    public ValueInput gameObjectInput;

    // Control flow port
    [DoNotSerialize]
    public ControlInput trigger;

    protected override void Definition()
    {
        gameObjectInput = ValueInput<GameObject>("GameObject");
        trigger = ControlInput("Trigger", DeleteChildren);
    }

    private ControlOutput DeleteChildren(Flow flow)
    {
        GameObject obj = flow.GetValue<GameObject>(gameObjectInput);
        if (obj != null)
        {
            // Loop through all children and destroy them
            for (int i = obj.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = obj.transform.GetChild(i);
                if (child != null)
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
            }
        }
        return null; // No output flow
    }
}