using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnlimitedScrollUI.Collection {
    public class CardCell : MonoBehaviour {
        public TextMeshProUGUI TMP_Text;
        public GameObject popup;
        public bool useInfoDisplay;
        public List<Sprite> spriteList;
        
        private InfoDisplay infoDisplay;
        private int index;

        public void SetTMPText(int newIndex) {
            index = newIndex;
            TMP_Text.text = $"{index}";
        }

        public void SetSprite(int newIndex) {
            index = newIndex;
            Image image = GetComponent<Image>();
            image.sprite = spriteList[index];
            
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
