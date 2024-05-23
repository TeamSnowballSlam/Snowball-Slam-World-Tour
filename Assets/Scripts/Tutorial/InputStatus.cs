using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputStatus : MonoBehaviour
{
    public bool actionComplete;
    public Image greenSprite;

    public bool holding;
    public bool ready;

    public inputType currentType;

    // Start is called before the first frame update
    void Start()
    {
        
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
        else if (ready && currentType == inputType.interact && greenSprite.fillAmount > 0)
        {
            greenSprite.fillAmount -= Time.deltaTime * 5f;
        }
        else
        {
            greenSprite.fillAmount = 0;
        }
        
    }

    public void UpdateValues(bool action, inputType type, bool hold, bool ready)
    {
        this.ready = ready;
        this.actionComplete = action;
        this.holding = hold;
        this.currentType = type;
    }
}
