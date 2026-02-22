using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void EndSession()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .Unload(SceneDatabase.Slots.Session)
            .Unload(SceneDatabase.Slots.SessionContent)
            .WithClearUnusedAssets()
            .WithOverlay()
            .Perform();
    }

    public void SwitchToCollection()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Collection, setActive: true)
            .WithOverlay()
            .Perform();
    }
}
