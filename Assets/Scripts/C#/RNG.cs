using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RNG : MonoBehaviour
{
    [SerializeField] public List<float> tierPercentages;
    [SerializeField] public List<string> tierRarities;
    [SerializeField] public string selectedRarity;
    [SerializeField] public int selectedTier;

    // State variables for Xorshift256++
    private ulong[] state = new ulong[4];

    void Start()
    {
    }

    public void GenerateTier()
    {
        GenerateArray();

        selectedTier = GetTier(tierPercentages);
        selectedRarity = SelectedRarity(selectedTier);
        Debug.Log("Selected Tier: " + selectedRarity);
    }

    public string SelectedRarity(int tier)
    {
        string rarity = tierRarities[tier];
        return rarity;
    }

    void GenerateArray()
    {
    // Get the current time in ticks (high precision)
    long timeTicks = DateTime.UtcNow.Ticks;
    // Get high-resolution stopwatch timestamp
    long stopwatchTicks = System.Diagnostics.Stopwatch.GetTimestamp();
    // Combine with device-specific info
    string deviceInfo = SystemInfo.deviceUniqueIdentifier;
    // Concatenate all parts into a single string
    string input = deviceInfo + timeTicks.ToString() + stopwatchTicks.ToString();
    // Hash the combined string
    byte[] hashBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

    // Fill state array with parts of the hash
    for (int i = 0; i < 4; i++)
    {
        state[i] = BitConverter.ToUInt64(hashBytes, i * 8);
        // Ensure no zero states to avoid degenerate sequences
        if (state[i] == 0) state[i] = 0xDEADBEEFCAFEBABE ^ (ulong)i;
    }
    }

    // Xorshift256++ algorithm
    ulong Xorshift256Plus()
    {
        ulong s0 = state[0];
        ulong s1 = state[1];
        ulong s2 = state[2];
        ulong s3 = state[3];

        ulong result = s0 + s3;

        s1 ^= s1 << 23; // a
        s2 ^= s2 >> 17; // b
        s3 ^= s3 << 26; // c

        state[0] = s1;
        state[1] = s2;
        state[2] = s3;
        state[3] = s0;

        return result;
    }

    float GetRandomFloat()
    {
        // Generate a float between 0 and 100
        ulong randInt = Xorshift256Plus();
        // Normalize to [0,1]
        float normalized = (randInt & 0xFFFFFFFFFFFF) / (float)0x1000000000000; // 2^48
        return normalized * 100f;
    }

    int GetTier(List<float> percentages)
    {
        int maxAttempts = 10; // prevent infinite loops
        int attempts = 0;
        int selectedTier = -1;

        while (attempts < maxAttempts)
        {
            float randValue = GetRandomFloat();
            float cumulative = 0f;

            for (int i = 0; i < percentages.Count; i++)
            {
                if (percentages[i] == 0f)
                    continue; // skip zero percentage options

                cumulative += percentages[i];

                if (randValue <= cumulative)
                {
                    selectedTier = i;
                    break;
                }
            }

            if (selectedTier != -1)
            {
            // Successfully selected a non-zero percentage tier
                return selectedTier;
            }

            attempts++;
        }

        // If no non-zero tier was selected after max attempts, fallback
        return 0;
    }
}