using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventTrigger : MonoBehaviour
{

    public List<UnityEngine.Events.UnityEvent> eventsToTrigger;

    public void OnButtonClicked()
    {
        foreach (var eventToTrigger in eventsToTrigger)
        {
            eventToTrigger.Invoke();
        }
    }
}