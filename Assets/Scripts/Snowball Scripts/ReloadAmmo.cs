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

    // GameObjects
    private GameObject snowTray;
    private GameObject reloadMeter;


    // UI Components
    private Slider sliderComponent;
    public TextMeshProUGUI trayAmountText; //used in snowmachine.cs

    // Constants
    private static int MAXAMMO = 5;

    private int amount;

    void Start()
    {
        snowInventory = gameObject.GetComponent<SnowInventory>();
        trayAmountText = snowTray.transform.Find("Canvas/Amount").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// When the player enters the collider of the snow pile, they can reload their ammo.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Snow Tray")
        {
            canReload = true;

            snowTray = other.gameObject; // finds the specific snow tray
            sliderComponent = snowTrayInv.meter.GetComponent<Slider>();
        }
    }

    /// <summary>
    /// When the player exits the collider of the snow pile, they can no longer reload their ammo.
    /// </summary>
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Snow Tray")
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
            if (context.started && snowTrayInv.Inventory >= 1) // when the hold has started and there is snow in the tray
            {
                reloadMeter.SetActive(true); // show the meter when used
                isReloading = true; // for the update
            }
            else if (context.canceled) // when the hold has ended or interrupted
            {
                sliderComponent.value = 0; // reset the slider value
                isReloading = false;
                reloadMeter.SetActive(false); // hides the meter
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

                snowInventory.CurrentAmmo += amount; // adds the difference to the player's ammo

                // turn off
                isReloading = false;
                sliderComponent.value = 0;
                reloadMeter.SetActive(false);

                snowTrayInv.Inventory -= amount; // 
                trayAmountText.text = snowTrayInv.Inventory.ToString();
            }
        }
    }

    private void SnowToTake()
    {
        int difference = MAXAMMO - snowInventory.CurrentAmmo; // the difference between the max ammo and the current ammo
        if (snowTrayInv.inventory >= difference) // if the tray has more snow than the difference / less than max
        {
            amount = difference; // the amount to take is the difference
        }
        else
        {
            amount = snowTrayInv.inventory; // else take the rest
        }

    }
}
