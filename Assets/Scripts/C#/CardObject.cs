using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

public class CardObject : MonoBehaviour
{
    public TextMeshProUGUI Slot_Text;
    public TextMeshProUGUI Count_Text;
    public bool isOwned;
    public GameObject content;
    public string cardName;
    public string lang;
    public string source;
    public int cardCount = 0;
    
    public void SetContent()
    {
        if (transform.parent != null)
        {
            content = transform.parent.gameObject;  
        } 
    }

    public void SetSlotText() 
    {
        int index = gameObject.transform.GetSiblingIndex();
        Slot_Text.text = $"{index}";
    }

    public void SetCountText() 
    {
        Count_Text.text = $"{cardCount}";
    }
        
    public void GetOwned()
    {
        Image image = GetComponent<Image>();
        cardName = image.sprite.name;
        string countString = "count";
        string path = Path.Combine(Application.persistentDataPath, "CollectionData.json");
        string tokenPath = $"{lang}.{source}.{cardName}.{countString}";
        //Debug.Log($"{tokenPath}");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JObject jsonObject = JObject.Parse(json);
            JToken token = jsonObject.SelectToken(tokenPath);
            if (token != null)
            {
                string numberString = token.ToString();
                if (int.TryParse(numberString, out int number))
                {
                    isOwned = number > 0;
                    cardCount = number;
                }
                else
                {
                    Debug.Log($"Failed to parse token {cardName} to int");
                }
            }
            else
            {
                Debug.Log($"{cardName} Token is null.");
            }
        }
        else
        {
            Debug.Log($"File not found at path {path}");
        }
    }
}