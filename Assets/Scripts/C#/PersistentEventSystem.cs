using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentEventSystem : MonoBehaviour
{
    public static PersistentEventSystem eventSystem;

    void Awake() 
    {
        if (eventSystem == null)
        {
            if (this.name == "EventSystem")
            {
                eventSystem = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }      
    }
}
