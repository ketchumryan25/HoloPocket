using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Coffee.UIEffects;
using Unity.VisualScripting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class UIEffectSwitch : MonoBehaviour
{
    public bool isOwned;
    public UIEffect effect;
    public GameObject thisEffect;
    public UIEffectSwitch thisScript;

    public void Awake()
    {
        thisEffect = this.gameObject;
        thisScript = thisEffect.GetComponent<UIEffectSwitch>();
        effect = thisEffect.GetComponent<UIEffect>();
    }

    public void SetUIEffect()
    {
        if (thisScript.isOwned)
        {
            effect.enabled = false;
        }
        else
        {
            effect.enabled = true;
        }
    }
}
