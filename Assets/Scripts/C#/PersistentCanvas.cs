using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    public static PersistentCanvas canvas;

    void Awake() 
    {
        if (canvas == null)
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
    }
}
