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

    // Update is called once per frame
    void Update()
    {
        
    }
}
