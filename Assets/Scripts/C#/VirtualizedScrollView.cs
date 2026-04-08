using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualizedScrollView : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentTransform;
    public GameObject itemPrefab; // Prefab with a Text component for demo
    public int totalItemCount = 1000; // total data items
    public int maxVisibleItems = 20; // buffer size
    public int cacheSize = 30; // cache size

    private Dictionary<int, GameObject> indexToGameObject = new Dictionary<int, GameObject>();
    private LRUCache<string, GameObject> cache;

    private float itemHeight;
    private float viewportHeight;

    private float[] itemPositions;

    void Start()
    {
        cache = new LRUCache<string, GameObject>(cacheSize);

        // Get GridLayoutGroup cell size
        var layout = contentTransform.GetComponent<GridLayoutGroup>();
        itemHeight = layout.cellSize.y;

        viewportHeight = scrollRect.viewport.rect.height;

        // Precompute item positions
        itemPositions = new float[totalItemCount];
        for (int i = 0; i < totalItemCount; i++)
        {
            itemPositions[i] = -i * itemHeight; // assuming top-down
        }

        // Subscribe to scroll change
        scrollRect.onValueChanged.AddListener((v) => UpdateVisibleItems());

        // Initial update
        UpdateVisibleItems();
    }

    void UpdateVisibleItems()
    {
        float contentY = contentTransform.anchoredPosition.y;

        float viewportBottom = contentY;
        float viewportTop = contentY + viewportHeight;

        List<int> visibleIndices = new List<int>();

        // Find visible items
        for (int i = 0; i < totalItemCount; i++)
        {
            float itemPos = itemPositions[i];

            if (itemPos + itemHeight >= viewportBottom && itemPos <= viewportTop)
            {
                visibleIndices.Add(i);
            }
        }

        HashSet<int> activeIndices = new HashSet<int>(visibleIndices);

        // Show or create GameObjects for visible items
        foreach (int index in visibleIndices)
        {
            if (!indexToGameObject.TryGetValue(index, out GameObject go))
            {
                string key = "item_" + index;
                go = cache.Get(key);
                if (go == null)
                {
                    go = Instantiate(itemPrefab, contentTransform);
                }
                go.transform.localPosition = new Vector3(0, itemPositions[index], 0);
                go.SetActive(true);
                indexToGameObject[index] = go;

                // Update content
                UpdateItemContent(go, index);
            }
        }

        // Hide items not in view
        List<int> keysToRemove = new List<int>();
        foreach (var kvp in indexToGameObject)
        {
            if (!activeIndices.Contains(kvp.Key))
            {
                kvp.Value.SetActive(false);
                // Optionally, remove from dictionary if you want to keep it clean
                // keysToRemove.Add(kvp.Key);
            }
        }
        // foreach (int key in keysToRemove)
        // {
        //     indexToGameObject.Remove(key);
        // }
    }

    void UpdateItemContent(GameObject go, int index)
    {
        // Example: set text to display index
        Text txt = go.GetComponentInChildren<Text>();
        if (txt != null)
        {
            txt.text = "Item " + index;
        }
    }
}

// Simple LRU Cache implementation
public class LRUCache<TKey, TValue>
{
    private readonly int capacity;
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> cacheMap;
    private readonly LinkedList<KeyValuePair<TKey, TValue>> usageOrder;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cacheMap = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
        usageOrder = new LinkedList<KeyValuePair<TKey, TValue>>();
    }

    public TValue Get(TKey key)
    {
        if (cacheMap.TryGetValue(key, out var node))
        {
            // Move to front
            usageOrder.Remove(node);
            usageOrder.AddFirst(node);
            return node.Value.Value;
        }
        return default(TValue);
    }

    public void Add(TKey key, TValue value)
    {
        if (cacheMap.TryGetValue(key, out var existingNode))
        {
            usageOrder.Remove(existingNode);
        }
        else if (cacheMap.Count >= capacity)
        {
            // Remove least recently used
            var lru = usageOrder.Last;
            if (lru != null)
            {
                cacheMap.Remove(lru.Value.Key);
                usageOrder.RemoveLast();
            }
        }
        var newNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
        usageOrder.AddFirst(newNode);
        cacheMap[key] = newNode;
    }
}