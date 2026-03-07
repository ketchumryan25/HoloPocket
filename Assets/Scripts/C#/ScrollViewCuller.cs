using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollViewCuller : MonoBehaviour
{
    [SerializeField] private RectTransform viewportRect;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private List<Image> itemImages;

    void LateUpdate()
    {
        foreach (var image in itemImages)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, GetComponent<RectTransform>().position);
            bool isVisible = RectTransformUtility.RectangleContainsScreenPoint(
                viewportRect,
                screenPoint,
                null
            );
            image.enabled = isVisible;
        }
    }
}   