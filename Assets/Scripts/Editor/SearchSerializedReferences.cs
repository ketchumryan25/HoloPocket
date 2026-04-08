using UnityEditor;
using UnityEngine;
using System.IO;

public class SearchSerializedReferences
{
    [MenuItem("Tools/Find UIEffect References")]
    public static void FindReferences()
    {
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string path in assetPaths)
        {
            if (path.EndsWith(".prefab") 
                || path.EndsWith(".asset") 
                || path.EndsWith(".unity") 
                || path.EndsWith(".scene") 
                || path.EndsWith(".cs"))
            {
                string text = File.ReadAllText(path);
                if (text.Contains("UIEffect"))
                {
                    Debug.Log($"Found reference in: {path}");
                }
            }
        }
    }
}