using UnityEngine;
using UnityEngine.UI;

public class ScrollViewCullByX : MonoBehaviour
{
    [SerializeField] public RectTransform content;
    [SerializeField] public float deactivateX = -100f; // Deactivate when content's x < this value

    private RectTransform rectTransform;
    private bool wasActive = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        if (content == null) content = GetComponentInParent<ScrollRect>().content;
    }

    void Update()
    {
        float contentLeft = content.anchoredPosition.x;

        bool shouldBeActive = contentLeft >= deactivateX;

        if (shouldBeActive != wasActive)
        {
            gameObject.SetActive(shouldBeActive);
            wasActive = shouldBeActive;
        }
    }
}   