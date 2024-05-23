using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputStatus : MonoBehaviour
{
    public bool actionComplete;
    public Sprite redSprite, greenSprite;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = actionComplete ? greenSprite : redSprite;
    }
}
