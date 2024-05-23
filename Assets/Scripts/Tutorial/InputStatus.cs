using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputStatus : MonoBehaviour
{
    public bool actionComplete;
    public Image greenSprite;
    Image image;

    public bool holding;

    // Start is called before the first frame update
    void Start()
    {
        this.image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionComplete)
        {
            greenSprite.fillAmount = 1;
        }
        else if (holding)
        {
            greenSprite.fillAmount += Time.deltaTime * .5f;
        }
        else
        {
            greenSprite.fillAmount = 0;
        }
        
    }
}
