using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Globalization;

[UnitCategory("Time")]
[UnitTitle("Time Diff (Days)")]
public class TimeDiffDaysUnit : Unit
{
    // Input ports for the two time strings
    [DoNotSerialize]
    public ValueInput timeString1;

    [DoNotSerialize]
    public ValueInput timeString2;

    // Output port for the difference in days
    [DoNotSerialize]
    public ValueOutput differenceInDays;

    protected override void Definition()
    {
        timeString1 = ValueInput<string>("Time1");
        timeString2 = ValueInput<string>("Time2");
        differenceInDays = ValueOutput<float>("Difference (Days)", GetDifference);
        Requirement(timeString1, differenceInDays);
        Requirement(timeString2, differenceInDays);
    }

    private float GetDifference(Flow flow)
    {
        string timeStr1 = flow.GetValue<string>(timeString1);
        string timeStr2 = flow.GetValue<string>(timeString2);

        DateTime dt1, dt2;
        string format = "yyyy/MM/dd H:mm:ss";

        if (DateTime.TryParseExact(timeStr1, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1) &&
            DateTime.TryParseExact(timeStr2, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2))
        {
            TimeSpan diff = dt2 - dt1;
            return Mathf.FloorToInt((float)diff.TotalDays); // Changed from TotalSeconds to TotalDays
        }
        else
        {
            Debug.LogWarning("Invalid time format. Expected format: " + format);
            return 0f;
        }
    }
}