using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteFromAtlas : MonoBehaviour
{

    [SerializeField] SpriteAtlas atlas;
    [SerializeField] string spriteName;

    // Start is called before the first frame update
    public void GetSprite()
    {
        GetComponent<Image>().sprite = atlas.GetSprite(spriteName);
    }

}
