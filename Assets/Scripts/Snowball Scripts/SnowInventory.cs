/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// Manages the player's snowball inventory. Stored on each player.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnowInventory : MonoBehaviour
{
    private int currentAmmo;
    public int CurrentAmmo
    {
        get
        { 
            return currentAmmo;
            Debug.Log("Current ammo: " + currentAmmo);
        }
        set
        {
            currentAmmo = value;
        }
    }
    private string penguinName;

    void Start()
    {
        penguinName = gameObject.name; // for debugging purposes
        currentAmmo = 5;
    }
}
