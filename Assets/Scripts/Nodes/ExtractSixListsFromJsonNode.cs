using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;
using Newtonsoft.Json.Linq; // Requires Newtonsoft.Json

[UnitTitle("Extract 6 Lists from JSON")]
[UnitCategory("Custom")]
public class ExtractSixListsFromJsonNode : Unit
{
    // Input: JSON data string
    [DoNotSerialize]
    public ValueInput jsonDataInput;

    // Outputs: six lists of strings
    [DoNotSerialize]
    public ValueOutput list1Output;
    [DoNotSerialize]
    public ValueOutput list2Output;
    [DoNotSerialize]
    public ValueOutput list3Output;
    [DoNotSerialize]
    public ValueOutput list4Output;
    [DoNotSerialize]
    public ValueOutput list5Output;
    [DoNotSerialize]
    public ValueOutput list6Output;

    protected override void Definition()
    {
        jsonDataInput = ValueInput<string>("JSON Data");

        list1Output = ValueOutput<List<string>>("Column 1", GetList1);
        list2Output = ValueOutput<List<string>>("Column 2", GetList2);
        list3Output = ValueOutput<List<string>>("Column 3", GetList3);
        list4Output = ValueOutput<List<string>>("Column 4", GetList4);
        list5Output = ValueOutput<List<string>>("Column 5", GetList5);
        list6Output = ValueOutput<List<string>>("Masterwork", GetList6);
    }

    private List<string> GetList1(Flow flow) => GetListByKey(flow, "column1");
    private List<string> GetList2(Flow flow) => GetListByKey(flow, "column2");
    private List<string> GetList3(Flow flow) => GetListByKey(flow, "column3");
    private List<string> GetList4(Flow flow) => GetListByKey(flow, "column4");
    private List<string> GetList5(Flow flow) => GetListByKey(flow, "column5");
    private List<string> GetList6(Flow flow) => GetListByKey(flow, "masterwork");

    private List<string> GetListByKey(Flow flow, string key)
    {
        string jsonString = flow.GetValue<string>(jsonDataInput);
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("JSON data is null or empty");
            return null;
        }

        try
        {
            var jToken = JToken.Parse(jsonString);
            var arrayToken = jToken.SelectToken($"$..{key}");

            if (arrayToken != null && arrayToken.Type == JTokenType.Array)
            {
                var list = new List<string>();
                foreach (var item in arrayToken)
                {
                    list.Add(item.ToString());
                }
                return list;
            }
            else
            {
                Debug.LogWarning($"Key '{key}' not found or is not an array");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error parsing JSON: {e.Message}");
            return null;
        }
    }
}