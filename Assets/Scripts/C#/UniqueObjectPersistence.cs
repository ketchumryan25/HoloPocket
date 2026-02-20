using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueObjectPersistence : MonoBehaviour
{
    public static UniqueObjectPersistence uniqueInstance;

    void Awake() 
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    }
}
