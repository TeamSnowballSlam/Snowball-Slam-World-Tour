using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReloadAmmo : MonoBehaviour
{
    private SnowInventory snowInventory;
    private bool canReload = false;
    private GameObject snowPile;
    [SerializeField]private GameObject reloadSlider;
    private double timeHeld;
    private Slider sliderComponent;
    private bool isReloading = false;

    void Start()
    {
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
            reloadSlider.SetActive(true);
            //StartCoroutine(Reload(context));
            //timeHeld = context.time;
            //Debug.Log("Time Held: " + context.duration);
            //reloadSlider.GetComponent<Slider>().value = (float)context.time;
            //reloadSlider.GetComponent<Slider>().value = (float)context.duration;

            isReloading = true;
        }
    }

    void Update()
    {
        if (isReloading)
        {
            sliderComponent.value += Time.deltaTime;
            if (sliderComponent.value >= sliderComponent.maxValue)
            {
                snowInventory.currentAmmo += 1;
                isReloading = false;
                sliderComponent.value = 0;
                reloadSlider.SetActive(false);
            }
        }
    }

}
