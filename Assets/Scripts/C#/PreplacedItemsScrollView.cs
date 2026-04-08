using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreplacedItemsScrollView : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject contentObject;
    private GameObject viewport;
    private ScrollRect scrollRect;
    private RectTransform contentTransform;
    public List<GameObject> allItems; // All items pre-placed in scene, ordered by index
    
    private int totalItemCount;
    public int bufferCount = 5; // number of items to buffer before/after viewport

    private float itemHeight;
    private float viewportHeight;
    private float[] itemPositions;

    private HashSet<int> activeIndices = new HashSet<int>();

    void Awake()
    {
        if (contentObject == null)
        {
            contentObject = this.gameObject;
        }
        if (contentObject != null)
        {
            contentTransform = contentObject.GetComponent<RectTransform>();
        }

        if (scrollView == null)
        {
            Transform v = contentObject.transform.parent;
            viewport = v.gameObject;
            Transform s = viewport.transform.parent;
            scrollView = s.gameObject;
        }
        if (scrollView != null)
        {
            scrollRect = scrollView.GetComponent<ScrollRect>();
        }
        
        else
        {
            Debug.LogError("ScrollRect not found! Please assign or ensure it's in parent.");
        }
    }

    void Start()
    {
        totalItemCount = allItems.Count;

        if (contentTransform == null)
        {
            Debug.LogError("Content Transform is not assigned or found.");
            return;
        }

        var layout = contentTransform.GetComponent<GridLayoutGroup>();
        if (layout != null)
        {
            itemHeight = layout.cellSize.y;
        }
        else
        {
            Debug.LogError("GridLayoutGroup component missing on content. Please add one.");
            return;
        }

        viewportHeight = scrollRect.viewport.rect.height;

        // Precompute item positions
        itemPositions = new float[totalItemCount];
        for (int i = 0; i < totalItemCount; i++)
        {
            itemPositions[i] = -i * itemHeight; // assuming top-down layout
        }

        // Subscribe to scroll change
        if (scrollRect != null)
        {
            scrollRect.onValueChanged.AddListener((v) => UpdateVisibility());
            Debug.Log("ScrollRect listener added");
        }
        else
        {
            Debug.LogError("ScrollRect is null, cannot add listener");
        }

        // Initial update
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        if (contentTransform == null) return;

        Debug.Log("UpdateVisibility called");
        float contentY = contentTransform.anchoredPosition.y;

        float viewportBottom = contentY;
        float viewportTop = contentY + viewportHeight;

        int startIdx = 0;
        int endIdx = totalItemCount - 1;

        // Find start index
        for (int i = 0; i < totalItemCount; i++)
        {
            if (itemPositions[i] + itemHeight >= viewportBottom)
            {
                startIdx = Mathf.Max(0, i - bufferCount);
                break;
            }
        }

        // Find end index
        for (int i = totalItemCount - 1; i >= 0; i--)
        {
            if (itemPositions[i] <= viewportTop + bufferCount * itemHeight)
            {
                endIdx = Mathf.Min(totalItemCount - 1, i + bufferCount);
                break;
            }
        }

        HashSet<int> newActiveIndices = new HashSet<int>();

        // Activate visible items
        for (int i = startIdx; i <= endIdx; i++)
        {
            newActiveIndices.Add(i);
            if (!activeIndices.Contains(i))
            {
                // Activate item
                allItems[i].SetActive(true);
                allItems[i].transform.localPosition = new Vector3(0, itemPositions[i], 0);
                // Optional: update item content here
                //UpdateItemContent(allItems[i], i);
            }
        }

        // Deactivate non-visible items
        foreach (int i in activeIndices)
        {
            if (!newActiveIndices.Contains(i))
            {
                allItems[i].SetActive(false);
                Debug.LogWarning("Object Deactivated");
            }
        }

        activeIndices = newActiveIndices;
    }

    private void UpdateItemContent(GameObject item, int index)
    {
        // Example: set text to display index
        Text txt = item.GetComponentInChildren<Text>();
        if (txt != null)
        {
            txt.text = "Item " + index;
        }
    }
}