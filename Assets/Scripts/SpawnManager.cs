using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; //The two player spawn points
    private const string BASEPATH = "Penguins/";

    public void Start()
    {
        penguinSelect(GameSettings.Player1Penguin, "WASD");
        penguinSelect(GameSettings.Player2Penguin, "Arrows");
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.gameObject.GetComponent<PlayerDetails>().playerID = playerInput.playerIndex; //Setting the player ID to the player index
        playerInput.gameObject.GetComponent<PlayerDetails>().startingPosition = spawnPoints[playerInput.playerIndex].position; //Setting the player's starting position to the spawn point
    }

    private void penguinSelect(PenguinType penguinType, string scheme)
    {
        if (penguinType == PenguinType.None)
        {
            //Select a random penguin if there isn't one selected
            penguinType = (PenguinType)Random.Range(0, 2);
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
