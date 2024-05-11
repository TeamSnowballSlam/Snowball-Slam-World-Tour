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
    [SerializeField]private GameObject reloadSlider;
    private GameObject snowPile;

    // UI Components
    private Slider sliderComponent;
    private TextMeshProUGUI snowballText;

    // Constants
    private static int MAXAMMO = 5;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
        snowPile = GameObject.Find("Player Snow Pile");
        reloadSlider = snowPile.transform.Find("Canvas/Progress").gameObject;
        snowballText = GameObject.Find("Snowball Text").GetComponent<TextMeshProUGUI>();
        sliderComponent = reloadSlider.GetComponent<Slider>();
        reloadSlider.SetActive(false);
    }

    /// <summary>
    /// When the player enters the collider of the snow pile, they can reload their ammo.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player Snow Pile")
        {
            canReload = true;
        }
    }

    /// <summary>
    /// When the player exits the collider of the snow pile, they can no longer reload their ammo.
    /// </summary>
    void onTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player Snow Pile")
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
            if (context.started)
            {
                reloadSlider.SetActive(true);
                isReloading = true;
            }
            else if (context.canceled)
            {
                sliderComponent.value = 0;
                isReloading = false;
                reloadSlider.SetActive(false);
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
                reloadSlider.SetActive(false);
            }
        }
    }

}
