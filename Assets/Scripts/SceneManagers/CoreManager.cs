using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Title, SceneDatabase.Scenes.TitleScreen)
            .WithOverlay()
            .Perform();
    }

}
