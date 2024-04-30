using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnowInventory : MonoBehaviour
{
    [Header("Snowball Inventory")]
    private int maxAmmo = 5;
    public int currentAmmo = 5;
    [SerializeField] private GameObject ammoText;

    public void DecreaseAmmo()
    {
        currentAmmo--;
        ammoText.GetComponent<TextMeshProUGUI>().text = "Snowballs: " + currentAmmo.ToString();
    }
}
