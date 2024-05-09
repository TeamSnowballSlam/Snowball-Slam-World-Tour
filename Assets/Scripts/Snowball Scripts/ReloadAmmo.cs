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

    void Start()
    {
        snowPile = GameObject.Find("Player Snow Pile");
        reloadSlider = snowPile.transform.Find("Canvas/Progress").gameObject;
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
            timeHeld = context.time;
            reloadSlider.GetComponent<Slider>().value = (float)context.time;
        }
    }
}
