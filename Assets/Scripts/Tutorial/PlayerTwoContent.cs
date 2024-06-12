/// <remarks>
/// Author: Benjamin Mead
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This script enables or disables tutorial content relating to player two.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameSettings.Player2Exists) // this script disabled tutorial content relating to player two when it is not in game.
        {
            this.gameObject.SetActive(false);
        }
    }
}
