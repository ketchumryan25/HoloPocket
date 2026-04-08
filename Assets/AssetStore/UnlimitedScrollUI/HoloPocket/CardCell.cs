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

namespace UnlimitedScrollUI {
    public class CardCell : MonoBehaviour {
        public TextMeshProUGUI Slot_Text;
        public TextMeshProUGUI Count_Text;
        public GameObject popup;
        public bool useInfoDisplay;
        public bool isOwned;
        public GameObject content;
        public string cardName;
        public int cardCount = 0;
        
        private InfoDisplay infoDisplay;
        private GameObject spritesObject;
        private int index;

        public void SetContent()
        {
            if (transform.parent != null)
            {
                content = transform.parent.gameObject;  
            } 
        }

        public void SetSlotText(int newIndex) {
            index = newIndex;
            Slot_Text.text = $"{index}";
        }

        public void SetCountText() {
            Count_Text.text = $"{cardCount}";
        }

        public void SetSprite(int newIndex) 
        {
            index = newIndex;
            Image image = GetComponent<Image>();
            if (Variables.Object(content).IsDefined("Sprites"))
            {
                object obj = Variables.Object(content).Get("Sprites");
                if (obj is GameObject)
                {
                    GameObject contentSprites = (GameObject)obj;
                    if (contentSprites != null)
                    {
                        if (contentSprites.name == "EN_Cheer")
                        {
                            SpriteManager[] spriteManagers = contentSprites.GetComponents<SpriteManager>();
                            int count = spriteManagers.Length;
                            //Debug.Log("Count was " + count);
                            string targetID = "Cheer";
                            SpriteManager cheerManager = null;
                            foreach (var manager in spriteManagers)
                            {
                                if (manager.managerID == targetID)
                                {
                                    cheerManager = manager;
                                    break;
                                }
                            }
                            if (cheerManager != null)
                            {
                                Sprite cheer = cheerManager.GetSpriteByIndex(index);
                                image.sprite = cheer;
                            }

                        }
                        else
                        {
                            SpriteManager spritesManager = contentSprites.GetComponent<SpriteManager>();

                            Sprite sprite = spritesManager.GetSpriteByIndex(index);
                            image.sprite = sprite;
                        }
                    }
                }
            }
        }

        public void GetOwned()
        {
            Image image = GetComponent<Image>();
            cardName = image.sprite.name;
            CollectionScroller scroller = content.GetComponent<CollectionScroller>();
            string countString = "count";
            string path = Path.Combine(Application.persistentDataPath, "CollectionData.json");
            string tokenPath = $"{scroller.lang}.{scroller.source}.{cardName}.{countString}";
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

        public void SetupPopup(int newIndex) {
            index = newIndex;
            GetComponent<Button>().onClick.AddListener(() => {
                var instance = Instantiate(popup, GameObject.Find("Canvas").transform);
                instance.GetComponent<Popup>().text.text = $"You just clicked the cell {index}!";
            });
        }

        public void GetInfoDisplayer() {
            if (useInfoDisplay)
            {
                infoDisplay = FindObjectOfType<InfoDisplay>();
            }
        }

        public void ChangeCount(int count) {
            if (useInfoDisplay){
            if (!infoDisplay) return;
            
            infoDisplay.UpdateCellCount(count);}
        }

        public void DisplayVisibleText(ScrollerPanelSide side) {
            if (useInfoDisplay){
            if (!infoDisplay) return;
            
            var sideName = Enum.GetName(typeof(ScrollerPanelSide), side);
            infoDisplay.UpdateVisibleDisplay($"Cell {index} visible from {sideName}.");}
        }
        
        public void DisplayInvisibleText(ScrollerPanelSide side) {
            if (useInfoDisplay){
            if (!infoDisplay) return;
            
            var sideName = Enum.GetName(typeof(ScrollerPanelSide), side);
            infoDisplay.UpdateInvisibleDisplay($"Cell {index} invisible to {sideName}.");}
        }
    }
}
