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
            Debug.Log("More snowballs pleathe!");
            snowInventory = other.gameObject.GetComponent<SnowInventory>();
            Debug.Log("Before Snowballs: " + snowInventory.currentAmmo);
            snowInventory.currentAmmo = 5;
            Debug.Log("After Snowballs: " + snowInventory.currentAmmo);
        }
    }
}
