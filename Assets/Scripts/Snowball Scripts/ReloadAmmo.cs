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

    // Booleans
    private bool canReload = false;
    private bool isReloading = false;

    // GameObjects
    [SerializeField]private GameObject snowTray;
    [SerializeField]private GameObject reloadMeter;
    public SnowTrayInventory snowTrayInv;

    // UI Components
    private Slider sliderComponent;
    private TextMeshProUGUI snowballText;

    // Constants
    private static int MAXAMMO = 5;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
        snowTray = GameObject.Find("Snow Tray");
        reloadMeter = snowTray.transform.Find("Canvas/Progress").gameObject;
        sliderComponent = reloadMeter.GetComponent<Slider>();
        reloadMeter.SetActive(false);
        snowTrayInv = snowTray.GetComponent<SnowTrayInventory>();
        snowballText = GameObject.Find("Canvas/Snowball Text").GetComponent<TextMeshProUGUI>();
        snowballText.text = "Snowballs: " + snowInventory.currentAmmo.ToString();
    }

    /// <summary>
    /// When the player enters the collider of the snow pile, they can reload their ammo.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Snow Tray")
        {
            canReload = true;
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
        if (canReload)
        {
            if (context.started && snowTrayInv.inventory >= 5)
            {
                reloadMeter.SetActive(true);
                isReloading = true;
            }
            else if (context.canceled)
            {
                sliderComponent.value = 0;
                isReloading = false;
                reloadMeter.SetActive(false);
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
            sliderComponent.value += Time.deltaTime;
            if (sliderComponent.value >= sliderComponent.maxValue)
            {
                snowInventory.currentAmmo = MAXAMMO;
                snowballText.text = "Snowballs: " + snowInventory.currentAmmo.ToString();
                isReloading = false;
                sliderComponent.value = 0;
                reloadMeter.SetActive(false);

                snowTrayInv.inventory -= 5;
            }
        }
    }
}
