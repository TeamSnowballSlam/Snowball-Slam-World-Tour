/// <remarks>
/// Author: Erika Stuart
/// Date Created: 12/05/2024
/// Bugs: None known at this time.
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

    /// <summary>
    /// When the player enters the snowball machine's trigger, the player can interact with the machine.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger</param>
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Snowball Machine")
        {
            canInteract = true;
            snowMachine = other.gameObject.GetComponent<SnowMachine>(); // the specific snow machine
        }

    }

    /// <summary>
    /// When the player exits the snowball machine's trigger, the player can no longer interact with the machine.
    /// </summary>
    /// <param name="other">The collider of the object that exited the trigger</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Snowball Machine")
        {
            canInteract = false;
        }
    }
}
