using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTrayInventory : MonoBehaviour
{
    public int inventory;
    public GameObject meter;

    void Start()
    {
        meter = gameObject.transform.GetChild(0).GetChild(0).gameObject; // Canvas > Progress
        meter.SetActive(false);
    }
}
