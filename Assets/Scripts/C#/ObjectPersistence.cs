using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPersistence : MonoBehaviour
{
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
}
