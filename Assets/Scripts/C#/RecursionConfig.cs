using UnityEngine;
using Unity.VisualScripting;

public class RecursionConfig : MonoBehaviour
{
    public int depth = 300;
    void Awake()
    {
        Recursion.defaultMaxDepth = depth;
    }
    public void SetDepth()
    {
        Recursion.defaultMaxDepth = depth;
    }
}   