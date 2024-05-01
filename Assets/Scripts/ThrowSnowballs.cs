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

    public void ThrowSnowball(InputAction.CallbackContext context)
    {
        if(LevelManager.instance.roundOver || !LevelManager.instance.roundStarted) { return;}

        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(
            snowballPrefab,
            snowballPosition + transform.forward,
            Quaternion.identity
        ); // snowballPrefab is instantiated
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
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
