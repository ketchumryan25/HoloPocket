using UnityEngine;
using Unity.VisualScripting;

public class RecursionConfig : MonoBehaviour
{
    void Awake()
    {
        Recursion.defaultMaxDepth = 300;
    }
}   