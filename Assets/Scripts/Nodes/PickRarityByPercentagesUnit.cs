using UnityEngine;
using Unity.VisualScripting;

[UnitTitle("Pick Rarity by Percentages")]
[UnitCategory("Gameplay")]
public class PickRarityByPercentagesUnit : Unit
{
    // Inputs for each rarity percentage
    [DoNotSerialize]
    public ValueInput commonPercent;
    [DoNotSerialize]
    public ValueInput uncommonPercent;
    [DoNotSerialize]
    public ValueInput rarePercent;
    [DoNotSerialize]
    public ValueInput doubleRarePercent;
    [DoNotSerialize]
    public ValueInput specialPercent;
    [DoNotSerialize]
    public ValueInput superRarePercent;
    [DoNotSerialize]
    public ValueInput specialYellPercent;
    [DoNotSerialize]
    public ValueInput oshiSpecialRarePercent;
    [DoNotSerialize]
    public ValueInput ultraRarePercent;
    [DoNotSerialize]
    public ValueInput oshiUltraRarePercent;
    [DoNotSerialize]
    public ValueInput secretPercent;

    // Output: Selected rarity string
    [DoNotSerialize]
    public ValueOutput rarityOutput;

    protected override void Definition()
    {
        commonPercent = ValueInput<float>("Common");
        uncommonPercent = ValueInput<float>("Uncommon");
        rarePercent = ValueInput<float>("Rare");
        doubleRarePercent = ValueInput<float>("Double Rare");
        specialPercent = ValueInput<float>("Special");
        superRarePercent = ValueInput<float>("Super Rare");
        specialYellPercent = ValueInput<float>("Special Yell");
        oshiSpecialRarePercent = ValueInput<float>("Oshi Special Rare");
        ultraRarePercent = ValueInput<float>("Ultra Rare");
        oshiUltraRarePercent = ValueInput<float>("Oshi Ultra Rare");
        secretPercent = ValueInput<float>("Secret");
        
        rarityOutput = ValueOutput<string>("Selected Rarity", PickRarity);
    }

    private string PickRarity(Flow flow)
    {
        float[] percentages = new float[11]
        {
            flow.GetValue<float>(commonPercent),
            flow.GetValue<float>(uncommonPercent),
            flow.GetValue<float>(rarePercent),
            flow.GetValue<float>(doubleRarePercent),
            flow.GetValue<float>(specialPercent),
            flow.GetValue<float>(superRarePercent),
            flow.GetValue<float>(specialYellPercent),
            flow.GetValue<float>(oshiSpecialRarePercent),
            flow.GetValue<float>(ultraRarePercent),
            flow.GetValue<float>(oshiUltraRarePercent),
            flow.GetValue<float>(secretPercent)
        };

        // Clamp percentages to 0-100
        for (int i = 0; i < percentages.Length; i++)
        {
            percentages[i] = Mathf.Clamp(percentages[i], 0f, 100f);
        }

        // Sum of percentages
        float total = 0f;
        foreach (float p in percentages)
        {
            total += p;
        }

        if (total <= 0f)
        {
            Debug.LogWarning("Total percentage is zero or negative. Defaulting to 'Common'.");
            return "Common";
        }

        float rand = Random.Range(0f, total);
        float cumulative = 0f;
        string[] rarities = new string[]
        {
            "Common", "Uncommon", "Rare", "DoubleRare", "Special", "SuperRare",
            "SpecialYell", "OshiSpecialRare", "UltraRare", "OshiUltraRare", "Secret"
        };

        for (int i = 0; i < percentages.Length; i++)
        {
            cumulative += percentages[i];
            if (rand <= cumulative)
            {
                return rarities[i];
            }
        }

        // Fallback
        return "Common";
    }
}