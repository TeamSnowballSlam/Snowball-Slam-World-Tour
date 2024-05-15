/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script allows the snow machine to produce snowballs.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SnowMachine : MonoBehaviour
{    
    private Slider sliderComponent;
    public SnowTrayInventory snowTrayInv;
    public ReloadAmmo reloadAmmo;
    public int prodSpeed;

    private static int FULLYSTOCKED = 15; // so it doesn't produce infinitely

    // Start is called before the first frame update
    void Start()
    {
        sliderComponent = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        snowTrayInv = transform.parent.Find("Snow Tray").GetComponent<SnowTrayInventory>();
        prodSpeed = 6;
    }

    // Continuous filling of its meter until the tray is fully stocked (15)
    void Update()
    {
        if (snowTrayInv.Inventory < FULLYSTOCKED)
        {
            sliderComponent.gameObject.SetActive(true);
            sliderComponent.value += Time.deltaTime / prodSpeed;
            if (sliderComponent.value >= sliderComponent.maxValue)
            {
                sliderComponent.value = 0;
                snowTrayInv.Inventory += 1;
            }
        }
        else
        {
            sliderComponent.gameObject.SetActive(false);
        }
    }
}
