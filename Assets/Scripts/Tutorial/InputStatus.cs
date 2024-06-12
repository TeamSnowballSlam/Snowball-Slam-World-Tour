/// <remarks>
/// Author: Benjamin Mead
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This script manages the input status of the player in the tutorial
/// </summary>
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

    /// <summary>
    /// Updates the values of the input status.
    /// </summary>
    /// <param name="action">Whether the action is complete</param>
    /// <param name="type">The type of input</param>
    /// <param name="hold">Whether the input is being held</param>
    /// <param name="ready">Whether the input is ready</param>
    public void UpdateValues(bool action, inputType type, bool hold, bool ready)
    {
        this.ready = ready;
        this.actionComplete = action;
        this.holding = hold;
        this.currentType = type;
    }
}
