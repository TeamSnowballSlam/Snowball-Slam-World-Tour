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

    [SerializeField]private TextMeshProUGUI snowballText;   

    private int currentAmmo;
    public int CurrentAmmo
    {
        get
        { 
            return currentAmmo;
        }
        set
        {
            currentAmmo = value;
            snowballText.text = "Snowballs: " + currentAmmo.ToString(); // changes text when updated
        }
    }
    private string penguinName;

    void Start()
    {
        penguinName = gameObject.name; // for debugging purposes
        currentAmmo = 5;
        snowballText = GameObject.Find("Snowball Text").GetComponent<TextMeshProUGUI>();
        snowballText.text = "Snowballs: " + currentAmmo.ToString();
    }
}
