using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityObjectPersistence : MonoBehaviour
{
    public static UnityObjectPersistence mainCamera;
    public static UnityObjectPersistence eventSystem;
    public static UnityObjectPersistence canvas;
    public static UnityObjectPersistence mainLight;

    void Awake() 
    {
        if (mainCamera == null)
        {
            if (this.name == "Main Camera")
            {
                mainCamera = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (eventSystem == null)
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
        else if (canvas == null)
        {
            if (this.name == "Canvas")
            {
                canvas = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (mainLight == null)
        {
            if (this.name.Contains("Light"))
            {
                mainLight = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }        
    }
}
