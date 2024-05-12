using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the player's snowball inventory. Stored on each player.
/// </summary>
public class SnowInventory : MonoBehaviour
{
    public int currentAmmo = 5;
    private string penguinName;
    private TextMeshProUGUI snowballText;

    void Start()
    {
        penguinName = gameObject.name; // for debugging purposes
        snowballText = GameObject.Find("Snowball Text").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Decreases the amount of ammo the player has.
    /// </summary>
    public void DecreaseAmmo()
    {
        currentAmmo--;
        snowballText.text = "Snowballs: " + currentAmmo.ToString();
    }
}
