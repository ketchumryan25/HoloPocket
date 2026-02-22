using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SwitchToMainMenu()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.MainMenu, setActive: true)
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
    public void SwitchToShop()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Shop, setActive: true)
            .WithOverlay()
            .Perform();
    }
    public void OpenBoosterPack()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.BoosterPackOpen, setActive: true)
            .WithOverlay()
            .Perform();
    }
}
