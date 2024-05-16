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
    public SnowInventory snowInventory;
    private Animator animator;

    void Start()
    {
        snowInventory = GetComponent<SnowInventory>();
        animator = GetComponent<Animator>();
        if (animator == null) //If the animator is null it's in the children
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Player
    public void ThrowSnowball(InputAction.CallbackContext context)
    {
        if (GameSettings.currentGameState == GameStates.PostGame) return; // if game is over, don't throw snowball
        if (snowInventory.CurrentAmmo <= 0) return; // if no ammo, don't throw snowball
        if (context.phase != InputActionPhase.Started) return; // only throw snowball once--when phase is started
        if (GetComponent<PlayerMovement>().IsSliding) return; // if player is sliding, don't throw snowball
       // animator.SetTrigger("doThrow"); // trigger animation
        snowballPosition = new Vector3(transform.position.x + 1.0f, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        ); // snowballPrefab is instantiated
        snowball.GetComponent<SnowballCollision>().owner = "Player"; // owner of snowball is the player
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
        snowInventory.CurrentAmmo--;
    }

    public void ThrowSnowball()
    {
        if (GameSettings.currentGameState == GameStates.PostGame) return; // if game is over, don't throw snowball
      //  animator.SetTrigger("doThrow"); // trigger animation
        snowballPosition = new Vector3(transform.position.x + 2.0f, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        ); // snowballPrefab is instantiated
        snowball.GetComponent<SnowballCollision>().owner = "Enemy"; // owner of snowball is the player

        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
    }
}
