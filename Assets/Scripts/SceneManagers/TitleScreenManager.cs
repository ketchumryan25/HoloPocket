using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void StartSession()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Session, SceneDatabase.Scenes.Session)
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithOverlay()
            .Perform();
    }
}
