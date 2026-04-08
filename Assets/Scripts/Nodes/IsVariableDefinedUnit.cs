using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;

[UnitCategory("Variables")]
[UnitTitle("Is Variable Defined")]
public class IsVariableDefinedUnit : Unit
{
    [DoNotSerialize]
    public ValueInput scope;

    [DoNotSerialize]
    public ValueInput variableName;

    [DoNotSerialize]
    public ValueInput gameObjectInput;

    [DoNotSerialize]
    public new ValueOutput isDefined;

    protected override void Definition()
    {
        scope = ValueInput<string>("scope", "Object");
        variableName = ValueInput<string>("variableName");
        gameObjectInput = ValueInput<GameObject>("gameObject");

        isDefined = ValueOutput<bool>("isDefined", (flow) =>
        {
            var scopeStr = flow.GetValue<string>(scope).ToLower();
            var name = flow.GetValue<string>(variableName);
            var obj = flow.GetValue<GameObject>(gameObjectInput);

            switch (scopeStr)
            {
                case "graph":
                    // Use flow.stack to get the current graph
                    var graphVars = Variables.Graph(flow.stack);
                    return graphVars.IsDefined(name);
                case "object":
                    if (obj != null)
                        return Variables.Object(obj).IsDefined(name);
                    else
                        return false;
                case "scene":
                    // Access scene variables
                    var scene = SceneManager.GetActiveScene();
                    return Variables.Scene(scene).IsDefined(name);
                case "saved":
                    return Variables.Saved.IsDefined(name);
                case "application":
                    return Variables.Application.IsDefined(name);
                default:
                    return false;
            }
        });
    }
}