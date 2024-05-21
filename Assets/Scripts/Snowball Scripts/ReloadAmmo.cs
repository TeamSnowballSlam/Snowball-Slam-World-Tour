/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script allows the player to reload their ammo from the snowball tray
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ReloadAmmo : MonoBehaviour
{
    // Classes
    public SnowInventory snowInventory; // The player's snowball inventory
    public SnowTrayInventory snowTrayInv; // The snow tray's snowball inventory
    
    // Booleans
    private bool canReload = false;
    private bool isReloading = false;

    // UI Components
    private Slider sliderComponent;
    private TextMeshProUGUI snowballText;

    // Constants
    private static int MAXAMMO = 5;

    // GameObjects
    private GameObject snowTray;

    private int amount;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
    }

    /// <summary>
    /// When the player enters the collider of the snow pile, they can reload their ammo.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Snowball Machine")
        {
            canReload = true;
            snowTray = other.gameObject; // finds the specific snow tray
            snowTrayInv = snowTray.GetComponent<SnowTrayInventory>(); // gets the snow tray's inventory
            sliderComponent = snowTrayInv.meter.GetComponent<Slider>();
            snowInventory = gameObject.GetComponent<SnowInventory>();
        }
    }

    /// <summary>
    /// When the player exits the collider of the snow pile, they can no longer reload their ammo.
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Snowball Machine")
        {
            canReload = false;
        }
    }

    /// <summary>
    /// Holding down the 'E' key for a certain amount of time will reload the player's ammo to max.
    /// </summary>
    public void OnHold(InputAction.CallbackContext context)
    {
        if (canReload) // if the player is in the collider of the snow pile
        {
            if (context.started
            && snowTrayInv.Inventory >= 1
            && snowInventory.CurrentAmmo < MAXAMMO) // when the hold has started and there is snow in the tray
            {
                snowTrayInv.meter.SetActive(true); // turn on the reload meter
                isReloading = true; // for the update
            }
            else if (context.canceled) // when the hold has ended or interrupted
            {
                sliderComponent.value = 0; // reset the slider value
                isReloading = false;
                snowTrayInv.meter.SetActive(false); // turn off the reload meter
            }
        }
    }

    /// <summary>
    /// Updates the slider value when the player is reloading.
    /// </summary>
    void Update()
    {
        if (isReloading)
        {
            sliderComponent.value += Time.deltaTime; // increases the slider value over time
            if (sliderComponent.value >= sliderComponent.maxValue) // if it reaches the end
            {
                SnowToTake(); // starts a difference calculation

                isReloading = false;
                sliderComponent.value = 0;

                snowInventory.CurrentAmmo += amount; // adds the difference to the player's ammo
                snowTrayInv.Inventory -= amount; // 
            }
        }
    }

    /// <summary>
    /// Calculates the amount of snow to take from the tray.
    /// </summary>
    private void SnowToTake()
    {
        int difference = MAXAMMO - snowInventory.CurrentAmmo; // the difference between the max ammo and the current ammo
        if (snowTrayInv.Inventory >= difference) // if the tray has more snow than the difference / less than max
        {
            amount = difference; // the amount to take is the difference
        }
        else
        {
            amount = snowTrayInv.Inventory; // else take the rest
        }

    }
}
