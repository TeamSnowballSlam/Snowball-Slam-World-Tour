/// <remarks>
/// Author: Erika Stuart
/// </remarks>
/// <summary>
/// Manages the player's snowball inventory. Stored on each player.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SnowInventory : MonoBehaviour
{
    [SerializeField] private Image levelManager;
    private TextMeshProUGUI ammoText1;
    private TextMeshProUGUI ammoText2;
    private int playerNumber;

    private int currentAmmo;
    public int CurrentAmmo
    {
        get
        { 
            return currentAmmo;
        }
        set
        {
            currentAmmo = value;
            if (playerNumber == 0)
            {
                if (ammoText1 == null)
                {
                    ammoText1 = GameObject.Find("P1 Snowballs").GetComponent<TextMeshProUGUI>();
                }
                ammoText1.text = currentAmmo.ToString();
            }
            if (playerNumber == 1)
            {
                if (ammoText2 == null)
                {
                    ammoText2 = GameObject.Find("P2 Snowballs").GetComponent<TextMeshProUGUI>();
                }
                ammoText2.text = currentAmmo.ToString();
            }
        }
    }

    void Start()
    {
        currentAmmo = 10;
        playerNumber = GetComponent<PlayerDetails>().playerID;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            //Workaround for the tutorial players being 2 and 3 because of input systems
            if (playerNumber == 2)
            {
                playerNumber = 0;
            }
            else if (playerNumber == 3)
            {
                playerNumber = 1;
            }
        }
        if (playerNumber == 0)
        {
            try
            {
                ammoText1 = GameObject.Find("P1 Snowballs").GetComponent<TextMeshProUGUI>();
                ammoText1.text = currentAmmo.ToString();
            }
            catch (System.Exception e)
            {
                Debug.Log("Error finding ammo text: " + e.Message);
            }
        }
        else
        {
            try
            {
                ammoText2 = GameObject.Find("P2 Snowballs").GetComponent<TextMeshProUGUI>();
                ammoText2.text = currentAmmo.ToString();
            }
            catch (System.Exception e)
            {
                Debug.Log("Error finding ammo text: " + e.Message);
            }
        }
    }
}
