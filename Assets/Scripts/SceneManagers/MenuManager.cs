using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SwitchToTitleScreen()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.TitleScreen, setActive: true)
            .WithOverlay()
            .Perform();
    }
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
    public void SwitchToWonderPick()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.WonderPick, setActive: true)
            .WithOverlay()
            .Perform();
    }
    public void SwitchToWonderPickOpen()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.WonderPickOpen, setActive: true)
            .WithOverlay()
            .Perform();
    }
    public void SwitchToBoosterPacks()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.BoosterPacks, setActive: true)
            .WithOverlay()
            .Perform();
    }
    public void SwitchToVendor()
    {
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.SessionContent, SceneDatabase.Scenes.Vendor, setActive: true)
            .WithOverlay()
            .Perform();
    }
}
