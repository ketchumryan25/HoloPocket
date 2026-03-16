using Unity.VisualScripting;
using UnityEngine;

[UnitTitle("Start Trigger When True")]
[UnitCategory("Custom")]
public class StartTriggerWhenTrueUnit : Unit
{

    // Control ports
    [DoNotSerialize]
    public ControlInput triggerInput;

    [DoNotSerialize]
    public ControlOutput triggerOutput;

    // Boolean input
    [DoNotSerialize]
    public ValueInput startCondition;

    protected override void Definition()
    {
        // Define port for trigger input
        triggerInput = ControlInput("Trigger", (flow) =>
        {
            bool shouldStart = flow.GetValue<bool>(startCondition);
            if (shouldStart)
            {
                // Continue flow if false
                return triggerOutput;
            }
            else
            {
                // If condition is true, do not continue flow
                // Optionally, you could add logging or other behavior here
                return null; // Stops the flow
            }
        });

        // Define port for trigger output
        triggerOutput = ControlOutput("Done");

        // Define boolean input
        startCondition = ValueInput<bool>("Start When True", false);

        // Connect control flow
        Succession(triggerInput, triggerOutput);
    }
}