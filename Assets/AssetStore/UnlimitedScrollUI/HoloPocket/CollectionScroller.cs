using System.Collections;
using UnityEngine;
using Newtonsoft.Json;

namespace UnlimitedScrollUI {
    public class CollectionScroller : MonoBehaviour {
        public GameObject cell;
        public bool autoGenerate;
        public int totalCount = 33;
        public string lang;
        public string source;

        private IUnlimitedScroller unlimitedScroller;

        public void Generate() {
            unlimitedScroller = GetComponent<IUnlimitedScroller>();
            unlimitedScroller.Generate(cell, totalCount, (index, iCell) => {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });
        }

        private void Start() {
            unlimitedScroller = GetComponent<IUnlimitedScroller>();
            // Wait until the scroller size was set by other layout controllers.
            if (autoGenerate) {
                StartCoroutine(DelayGenerate());
            }
        }

        private IEnumerator DelayGenerate() {
            yield return new WaitForEndOfFrame();
            unlimitedScroller.Generate(cell, totalCount, (index, iCell) => {
                var regularCell = iCell as RegularCell;
                if (regularCell != null) regularCell.onGenerated?.Invoke(index);
            });
        }
    }
}