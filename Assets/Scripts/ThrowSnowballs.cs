using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowballs : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    private GameObject snowball; // instantiate of snowballPrefab
    private Vector3 snowballPosition;

    // Start is called before the first frame update
    void Start()
    {
        snowballPosition = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // space is player fire
        {
            ThrowSnowball();
        }
    }

    private void ThrowSnowball()
    {
        snowball = Instantiate(snowballPrefab, snowballPosition + transform.forward, Quaternion.identity);
        snowball.AddComponent<SnowballCollision>();
        snowball.transform.position = snowballPosition + transform.forward;
        snowball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        snowball.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
    }
}
