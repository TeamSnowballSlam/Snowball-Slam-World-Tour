/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script allows the player to interact with the snowball machine.
/// </summary>
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
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snowball Machine")
        {
            canInteract = true;
            snowMachine = other.gameObject.GetComponent<SnowMachine>(); // the specific snow machine
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Snowball Machine")
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
               snowMachine.prodSpeed = 3; // faster production
           }
           else if(context.canceled)
           {
                snowMachine.prodSpeed = 6;
           }
        }
    }
}
