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
    private int inventory;
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

            if (inventory <= 15 && inventory > 10)
            {
                snowballPileFull.SetActive(true);
                snowballPileTwoThirds.SetActive(false);
                snowballPileOneThird.SetActive(false);
            }
            else if (inventory <= 10 && inventory > 5)
            {
                snowballPileFull.SetActive(false);
                snowballPileTwoThirds.SetActive(true);
                snowballPileOneThird.SetActive(false);
            }
            else if(inventory <= 5 && inventory > 0)
            {
                snowballPileFull.SetActive(false);
                snowballPileTwoThirds.SetActive(false);
                snowballPileOneThird.SetActive(true);
            }
        }
    }
    public GameObject meter;

    private GameObject snowballPileFull;
    private GameObject snowballPileTwoThirds;
    private GameObject snowballPileOneThird;

    void Start()
    {
        meter = transform.Find("Canvas/Progress").gameObject;
        trayAmountText = transform.Find("Canvas/Amount").GetComponent<TextMeshProUGUI>();
        meter.SetActive(false); // set false here so it only shows when used

        snowballPileFull = transform.Find("Snowball Pile Full").gameObject;
        snowballPileTwoThirds = transform.Find("Snowball Pile Two Thirds").gameObject;
        snowballPileOneThird = transform.Find("Snowball Pile One Third").gameObject;

        snowballPileFull.SetActive(false);
        snowballPileTwoThirds.SetActive(false);
    }
}
