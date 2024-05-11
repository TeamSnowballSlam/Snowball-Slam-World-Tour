using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowMachine : MonoBehaviour
{    
    private Slider sliderComponent;
    public ReloadAmmo reloadAmmo;

    // Start is called before the first frame update
    void Start()
    {
        sliderComponent = GameObject.Find("Snowball Machine/Canvas/Progress").GetComponent<Slider>();
        reloadAmmo = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<ReloadAmmo>();
    }

    // Update is called once per frame
    void Update()
    {
        sliderComponent.value += Time.deltaTime / 5;
        if (sliderComponent.value >= sliderComponent.maxValue)
        {
            sliderComponent.value = 0;
            reloadAmmo.snowTrayInv += 1;
        }
    }
}
