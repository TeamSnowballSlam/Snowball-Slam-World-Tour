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

    // Start is called before the first frame update
    void Start()
    {
        sliderComponent = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        snowTrayInv = transform.parent.Find("Snow Tray").GetComponent<SnowTrayInventory>();
        prodSpeed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        sliderComponent.value += Time.deltaTime / prodSpeed;
        if (sliderComponent.value >= sliderComponent.maxValue)
        {
            sliderComponent.value = 0;
            snowTrayInv.Inventory += 1;
        }
    }

}
