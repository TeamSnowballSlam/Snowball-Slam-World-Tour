/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// This script manages the player's snow tray inventory.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnowTrayInventory : MonoBehaviour
{
    private TextMeshProUGUI trayAmountText;
    public int inventory;
    public int Inventory
    {
        get
        {
            return inventory;
        }
        set
        {
            inventory = value;
            trayAmountText.text = inventory.ToString(); // text changes when updated
        }
    }
    public GameObject meter;

    void Start()
    {
        meter = gameObject.transform.GetChild(0).Find("Progress").gameObject;
        trayAmountText = gameObject.transform.GetChild(0).Find("Amount").GetComponent<TextMeshProUGUI>();
        meter.SetActive(false); // set false here so it only shows when used
    }
}
