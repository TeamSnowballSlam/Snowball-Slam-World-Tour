using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnowInventory : MonoBehaviour
{
    [Header("Snowball Inventory")]
    public int currentAmmo = 5;
    private GameObject ammoText;

    void Start()
    {
        ammoText = GameObject.Find("Ammo");
        ammoText.GetComponent<TextMeshProUGUI>().text = "Snowballs: " + currentAmmo.ToString();
    }

    public void DecreaseAmmo()
    {
        currentAmmo--;
        ammoText.GetComponent<TextMeshProUGUI>().text = "Snowballs: " + currentAmmo.ToString();
    }
}
