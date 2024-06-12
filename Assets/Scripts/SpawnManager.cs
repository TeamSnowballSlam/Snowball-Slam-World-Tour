/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 11/04/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This class is used to manage the spawning of players in the game.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; //The two player spawn points
    private const string BASEPATH = "Penguins/"; //Base path for the penguin prefabs within the resources folder
    public static SpawnManager Instance;
    // Start is called before the first frame update
    public void Start()
    {
        Instance = this;
        penguinSelect(GameSettings.Player1Penguin, "WASD");
        if (GameSettings.Player2Exists)
        {
            penguinSelect(GameSettings.Player2Penguin, "Arrows");
        }
    }

    /// <summary>
    /// Spawns player two if they don't already exist
    /// </summary>
    public void spawnPlayerTwo()
    {
        if (!GameSettings.Player2Exists)
        {
            penguinSelect(GameSettings.Player2Penguin, "Arrows");
            GameSettings.Player2Exists = true;
        }
    }

    /// <summary>
    /// Called when a player joins the game
    /// </summary>
    /// <param name="playerInput">The player input component of the player that joined</param>
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        try
        {
            playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex; //Setting the player ID to the player index
            playerInput.gameObject.GetComponent<PlayerDetails>().startingPosition = spawnPoints[playerInput.playerIndex].position; //Setting the player's starting position to the spawn point
        }
        catch (System.Exception e)
        {
            Debug.Log("Error setting player details: " + e.Message);
        }
    }

    /// <summary>
    /// Spawns a player of type 'penguinType' with the control scheme 'scheme'
    /// </summary>
    /// <param name="penguinType">Type of penguin selected</param>
    /// <param name="scheme">Control scheme used</param>
    private void penguinSelect(PenguinType penguinType, string scheme)
    {
        if (penguinType == PenguinType.None)
        {
            //Select a random penguin if there isn't one selected
            penguinType = (PenguinType)Random.Range(1, 3);
        }

        //Load the selected penguin prefab
        string path = BASEPATH + penguinType.ToString();
        GameObject penguinPrefab = Resources.Load(path) as GameObject;
        if (penguinPrefab != null)
        {
            //Instantiate the penguin with the selected control scheme
            PlayerInput player = PlayerInput.Instantiate(penguinPrefab, controlScheme: scheme, pairWithDevice: Keyboard.current);
        }
        else
        {
            Debug.LogWarning("Penguin prefab not found at path: " + path);
        }
    }
}
