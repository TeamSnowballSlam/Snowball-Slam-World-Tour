using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowballs : MonoBehaviour
{
    [SerializeField] private GameObject snowball;
    private Vector3 snowballPosition;

    // Start is called before the first frame update
    void Start()
    {
        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowSnowball();
        }
    }

    private void ThrowSnowball()
    {
        snowball = Instantiate(this.snowball, snowballPosition + transform.forward, Quaternion.identity);
        snowball.transform.position = snowballPosition + transform.forward;
        snowball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
    }
}
