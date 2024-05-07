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
        if(other.gameObject.name == "Player Snow Pile")
        {
            Debug.Log("Entered reload area");
            snowInventory = GetComponent<SnowInventory>();
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(HoldReload());
            }
        }
    }

    private IEnumerator HoldReload()
    {
        yield return new WaitForSeconds(3);
        snowInventory.currentAmmo += 1;
        Debug.Log("Updated snowballs: " + snowInventory.currentAmmo);
    }
}
