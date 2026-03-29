using UnityEngine;
using System.Collections.Generic;

// Optional: Serializable class to pair sprites with tags in the Inspector
[System.Serializable]
public class SpriteTagPair
{
    public string tag;
    public Sprite sprite;
}

public class SpriteManager : MonoBehaviour
{
    public string managerID;
    // List of tagged sprites visible in Inspector
    public List<SpriteTagPair> spriteList = new List<SpriteTagPair>();

    // Dictionary for fast lookup by tag (optional, for performance)
    public Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
    public Dictionary<string, List<Sprite>> spriteGroups = new Dictionary<string, List<Sprite>>();

    private void Awake()
    {
        // Build dictionary for O(1) tag lookup
        foreach (var pair in spriteList)
        {
            if (!string.IsNullOrEmpty(pair.tag) && pair.sprite != null)
            {
                if (!spriteDict.ContainsKey(pair.tag))
                {
                    spriteDict[pair.tag] = pair.sprite;
                }
                // For group lookup
                if (!spriteGroups.ContainsKey(pair.tag))
                {
                    spriteGroups[pair.tag] = new List<Sprite>();
                }
                spriteGroups[pair.tag].Add(pair.sprite);
            }
        }
    }

    // Get sprite by index
    public Sprite GetSpriteByIndex(int index)
    {
        if (index >= 0 && index < spriteList.Count)
        {
            return spriteList[index].sprite;
        }
        Debug.LogError($"Invalid index: {index}. Index out of range.");
        return null;
    }

    // Get sprite by tag
    public Sprite GetSpriteByTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            Debug.LogError("Tag cannot be null or empty.");
            return null;
        }

        if (spriteDict.TryGetValue(tag, out Sprite sprite))
        {
            return sprite;
        }

        Debug.LogError($"No sprite found with tag: {tag}");
        return null;
    }

    public List<Sprite> GetSpritesByTag(string tag)
{
    if (string.IsNullOrEmpty(tag))
    {
        Debug.LogError("Tag cannot be null or empty.");
        return new List<Sprite>();
    }

    if (spriteGroups.TryGetValue(tag, out List<Sprite> sprites))
    {
        return new List<Sprite>(sprites); // Return a copy for safety
    }

    Debug.LogWarning($"No sprites found with tag: {tag}");
    return new List<Sprite>();
}
}   