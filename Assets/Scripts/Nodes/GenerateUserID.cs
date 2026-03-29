using UnityEngine;
using Unity.VisualScripting;
using System.Security.Cryptography;
using System.Text;
using System;

[UnitTitle("Generate User ID")]
[UnitCategory("System")]
public class GenerateUserID : Unit
{
    // Output: User ID string
    [DoNotSerialize]
    public ValueOutput userID;

    protected override void Definition()
    {
        userID = ValueOutput<string>("User ID", GenerateID);
    }

    private string GenerateID(Flow flow)
    {
        string userLogin = Environment.UserName;

        // Get system info
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        string platformStr = GetPlatformString();

        // Hash each part
        string devicePart = GetHashSegment(deviceID, 8);
        string userPart = GetHashSegment(userLogin, 8);
        string platformPart = GetHashSegment(platformStr, 8);

        string UID = $"{devicePart}-{userPart}-{platformPart}";

        return UID;
    }

    private string GetPlatformString()
    {
        // Map Application.platform to a fixed string
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return "Windows";
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.OSXEditor:
                return "MacOS";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.Android:
                return "Android";
            // Add more platforms if needed
            default:
                return "OtherPlatform";
        }
    }

    private string GetHashSegment(string input, int length)
    {
        using (var sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha.ComputeHash(bytes);
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashString.Substring(0, length);
        }
    }
}