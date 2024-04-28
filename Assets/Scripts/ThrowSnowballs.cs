using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowballs : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    private GameObject snowball; // instantiate of snowballPrefab
    private Vector3 snowballPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // wasd player fire key is q
        {
            ThrowSnowball();
        }
    }

    private void ThrowSnowball()
    {
        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z); // thrown at face level
        snowball = Instantiate(snowballPrefab, snowballPosition + transform.forward, Quaternion.identity); // snowballPrefab is instantiated
        snowball.transform.position = snowballPosition + transform.forward; // snowball is positioned in front of player
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse); // snowball moves at a constant rate
    }
}
