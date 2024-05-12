using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowMachine : MonoBehaviour
{    
    private Slider sliderComponent;
    [SerializeField]private GameObject snowTray;
    public SnowTrayInventory snowTrayInv;

    // Start is called before the first frame update
    void Start()
    {
        sliderComponent = GameObject.Find("Snowball Machine/Canvas/Progress").GetComponent<Slider>();
        snowTrayInv = snowTray.GetComponent<SnowTrayInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderComponent.value += Time.deltaTime / 5;
        if (sliderComponent.value >= sliderComponent.maxValue)
        {
            sliderComponent.value = 0;
            snowTrayInv.inventory += 1;
            Debug.Log("Snow Tray: " + snowTrayInv.inventory);
        }
    }
}
