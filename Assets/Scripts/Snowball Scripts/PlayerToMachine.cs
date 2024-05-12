using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerToMachine : MonoBehaviour
{
    private bool canInteract;
    private SnowMachine snowMachine;

    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
        snowMachine = GameObject.Find("Snowball Machine").GetComponent<SnowMachine>();
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snowball Machine")
        {
            canInteract = true;
            Debug.Log("Can interact");
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
           if(context.started)
           {
               snowMachine.prodSpeed = 3;
               Debug.Log("Should be faster");
           }
           else if(context.canceled)
           {
                snowMachine.prodSpeed = 6;
           }
        }
    }
}
