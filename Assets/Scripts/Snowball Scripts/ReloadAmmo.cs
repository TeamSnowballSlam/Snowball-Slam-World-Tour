using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAmmo : MonoBehaviour
{
    private SnowInventory snowInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            snowInventory = other.gameObject.GetComponent<SnowInventory>();
            snowInventory.currentAmmo = 5;
        }
    }
}
