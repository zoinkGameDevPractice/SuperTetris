using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldImage : MonoBehaviour
{
    public static HoldImage instance;

    private void Awake()
    {
        instance = this;
    }

    Sprite sprite;

    public Sprite transparent;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transparent;
        SetSprite(sprite);
    }

    public void SetSprite(Sprite spr)
    {
        GetComponent<Image>().sprite = spr;
    }
}
