using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardGridVirtualizer : MonoBehaviour
{
    public RectTransform viewport; // The viewport rect
    public RectTransform content; // Content rect inside ScrollRect
    public GameObject cardPrefab; // Card prefab
    public int totalCards = 300; // Total number of cards
    public int columns = 8; // number of columns
    public int visibleRows = 3; // number of visible rows
    public float cardWidth = 100f;
    public float cardHeight = 150f;
    public float spacing = 10f;

    private int totalRows;
    private Dictionary<int, GameObject> activeCards = new Dictionary<int, GameObject>();
    private Queue<GameObject> pooledCards = new Queue<GameObject>();

    private float lastContentY = -Mathf.Infinity;

    void Start()
    {
        totalRows = Mathf.CeilToInt((float)totalCards / columns);
        // Set content size
        content.sizeDelta = new Vector2(
            columns * (cardWidth + spacing) - spacing,
            totalRows * (cardHeight + spacing) - spacing
        );

        // Initialize pool
        int poolSize = (visibleRows + 2) * columns; // extra buffer
        for (int i = 0; i < poolSize; i++)
        {
            GameObject card = Instantiate(cardPrefab, content);
            card.SetActive(false);
            pooledCards.Enqueue(card);
        }
    }

    void Update()
    {
        // Check if content has scrolled enough to update
        float contentY = content.anchoredPosition.y;
        if (Mathf.Abs(contentY - lastContentY) > (cardHeight + spacing) / 2)
        {
            lastContentY = contentY;
            UpdateVisibleCards();
        }
    }

    void UpdateVisibleCards()
    {
        float contentY = content.anchoredPosition.y;

        int firstVisibleRow = Mathf.FloorToInt(contentY / (cardHeight + spacing));
        firstVisibleRow = Mathf.Max(0, firstVisibleRow);
        int lastVisibleRow = firstVisibleRow + visibleRows + 1; // buffer
        lastVisibleRow = Mathf.Min(totalRows - 1, lastVisibleRow);

        HashSet<int> neededIndices = new HashSet<int>();

        // Spawn needed cards
        for (int row = firstVisibleRow; row <= lastVisibleRow; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int index = row * columns + col;
                if (index >= totalCards) continue;
                neededIndices.Add(index);
                if (!activeCards.ContainsKey(index))
                {
                    // Reuse pooled card
                    if (pooledCards.Count > 0)
                    {
                        GameObject card = pooledCards.Dequeue();
                        card.SetActive(true);
                        // Assign data here, e.g., sprite, text
                        // Example: card.GetComponent<Image>().sprite = yourSprites[index];

                        // Position the card
                        RectTransform rt = card.GetComponent<RectTransform>();
                        float x = col * (cardWidth + spacing);
                        float y = -row * (cardHeight + spacing);
                        rt.anchoredPosition = new Vector2(x, y);

                        activeCards.Add(index, card);
                    }
                    else
                    {
                        Debug.LogWarning("Pool exhausted! Increase pool size.");
                    }
                }
            }
        }

        // Recycle cards that are no longer needed
        List<int> keysToRemove = new List<int>();
        foreach (var kvp in activeCards)
        {
            if (!neededIndices.Contains(kvp.Key))
            {
                // Recycle
                GameObject card = kvp.Value;
                card.SetActive(false);
                pooledCards.Enqueue(card);
                keysToRemove.Add(kvp.Key);
            }
        }

        foreach (int index in keysToRemove)
        {
            activeCards.Remove(index);
        }
    }
}