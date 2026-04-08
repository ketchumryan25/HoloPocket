using UnityEditor;
using UnityEditor.UI;

namespace UnlimitedScrollUI {
    [CustomEditor(typeof(GridUnlimitedScroller), true)]
    [CanEditMultipleObjects]
    public class GridUnlimitedScrollerEditor : GridLayoutGroupEditor {
        private SerializedProperty matchContentWidth;
        private SerializedProperty cellPerRow;
        private SerializedProperty horizontalAlignment;
        private SerializedProperty cacheSize;
        private SerializedProperty scrollRect;
        private SerializedProperty cacheNode;

        protected override void OnEnable() {
            base.OnEnable();

            matchContentWidth = serializedObject.FindProperty("matchContentWidth");
            cellPerRow = serializedObject.FindProperty("cellPerRow");
            horizontalAlignment = serializedObject.FindProperty("horizontalAlignment");
            cacheSize = serializedObject.FindProperty("cacheSize");
            scrollRect = serializedObject.FindProperty("scrollRect");
            cacheNode = serializedObject.FindProperty("cacheNode");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(matchContentWidth, true);
            if (!matchContentWidth.boolValue) {
                EditorGUILayout.PropertyField(cellPerRow, true);
            }

            EditorGUILayout.PropertyField(horizontalAlignment, true);
            EditorGUILayout.PropertyField(cacheSize, true);
            EditorGUILayout.PropertyField(scrollRect, true);
            EditorGUILayout.PropertyField(cacheNode, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
