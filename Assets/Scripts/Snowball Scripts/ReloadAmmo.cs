using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReloadAmmo : MonoBehaviour
{
    public SnowInventory snowInventory;
    private bool canReload = false;
    private GameObject snowPile;
    [SerializeField]private GameObject reloadSlider;
    private double timeHeld;
    private Slider sliderComponent;
    private bool isReloading = false;
    private static int MAXAMMO = 5;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
        snowPile = GameObject.Find("Player Snow Pile");
        reloadSlider = snowPile.transform.Find("Canvas/Progress").gameObject;
        sliderComponent = reloadSlider.GetComponent<Slider>();
        reloadSlider.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player Snow Pile")
        {
            canReload = true;
        }
    }

    void onTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player Snow Pile")
        {
            canReload = false;
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (canReload)
        {
            if (context.started)
            {
                timeHeld = context.time;
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

    void Update()
    {
        if (isReloading)
        {
            sliderComponent.value += Time.deltaTime;
            if (sliderComponent.value >= sliderComponent.maxValue)
            {
                snowInventory.currentAmmo = MAXAMMO;
                Debug.Log("Snowballs: " + snowInventory.currentAmmo);
                isReloading = false;
                sliderComponent.value = 0;
                reloadSlider.SetActive(false);
            }
        }
    }

}
