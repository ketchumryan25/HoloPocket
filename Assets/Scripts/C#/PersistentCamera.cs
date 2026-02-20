using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCamera : MonoBehaviour
{
    public static PersistentCamera mainCamera;

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
    }
}
