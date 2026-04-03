using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGrayscaleToggle : MonoBehaviour
{
    public Material uiMaterial;

    public Toggle grayscaleToggle;

    void Start()
    {
        if (grayscaleToggle != null && uiMaterial != null)
        {
            // Set initial toggle state based on material's current _GrayScale value
            float currentGrayScale = uiMaterial.GetFloat("_GrayScale");
            grayscaleToggle.isOn = currentGrayScale > 0.5f;

            // Add listener to toggle value change
            grayscaleToggle.onValueChanged.AddListener(OnToggleChanged);
        }
        else
        {
            Debug.LogWarning("UIGrayscaleToggle: Assign uiMaterial and grayscaleToggle in inspector");
        }
    }

    void OnToggleChanged(bool isOn)
    {
        if (uiMaterial != null)
        {
            // Set the _GrayScale property in the material
            uiMaterial.SetFloat("_GrayScale", isOn ? 1f : 0f);
        }
    }
}