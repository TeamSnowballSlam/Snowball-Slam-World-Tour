/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script allows the player to throw snowballs.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThrowSnowballs : MonoBehaviour
{
    [SerializeField]
    private GameObject snowballPrefab;
    private GameObject snowball; // instantiate of snowballPrefab
    private Vector3 snowballPosition;
    private SnowInventory snowInventory;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
    }

    public void ThrowSnowball(InputAction.CallbackContext context)
    {
        if (snowInventory.CurrentAmmo <= 0) return; // if no ammo, don't throw snowball
        if (context.phase != InputActionPhase.Started) return; // only throw snowball once--when phase is started
        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        ); // snowballPrefab is instantiated
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
        snowInventory.CurrentAmmo--;
    }

    public void ThrowSnowball()
    {
        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        ); // snowballPrefab is instantiated
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
    }
}
