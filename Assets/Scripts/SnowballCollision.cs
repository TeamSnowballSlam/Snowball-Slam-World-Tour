using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowballCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject); //destroys itself no matter what it hits, snowball or border

    }
}
