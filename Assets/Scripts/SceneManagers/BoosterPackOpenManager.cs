using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPackOpenManager : MonoBehaviour
{
    public void SwitchToCollection()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Collection, setActive: true)
            .WithOverlay()
            .Perform();
    }
}
