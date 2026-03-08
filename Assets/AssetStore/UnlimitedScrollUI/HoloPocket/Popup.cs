using UnityEngine;
using UnityEngine.UI;

namespace UnlimitedScrollUI.Collection {
    public class Popup : MonoBehaviour {
        public Text text;
        public Button btn;

        private void Start() {
            btn.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}
