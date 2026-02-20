using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentLight : MonoBehaviour
{
    public static PersistentLight mainLight;

    void Awake() 
    {
        if (mainLight == null)
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
