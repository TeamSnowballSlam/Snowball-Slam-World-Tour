using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the penguin type for player 1
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP1(PenguinType penguinType)
    {
        GameSettings.Player1Penguin = penguinType;
    }

    /// <summary>
    /// Sets the penguin type for player 2
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP2(PenguinType penguinType)
    {
        GameSettings.Player2Penguin = penguinType;
    }

    /// <summary>
    /// Toggles the player 2 exists boolean
    /// </summary>
    public void TogglePlayer2()
    {
        GameSettings.Player2Exists = !GameSettings.Player2Exists;
        if (!GameSettings.Player2Exists)
        {
            GameSettings.Player2Penguin = PenguinType.None;
        }
    }
}
