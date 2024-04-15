using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject player2Select;
    // Start is called before the first frame update
    void Start()
    {
        player2Select.SetActive(false);
    }

    /// <summary>
    /// Sets the penguin type for player 1
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP1(string penguinType)
    {
        switch (penguinType)
        {
            case "Hoiho":
                GameSettings.Player1Penguin = PenguinType.Hoiho;
                break;
            case "Kororā":
                GameSettings.Player1Penguin = PenguinType.Kororā;
                break;
            case "Tawaki":
                GameSettings.Player1Penguin = PenguinType.Tawaki;
                break;
            default:
                GameSettings.Player1Penguin = PenguinType.None;
                break;
        }
    }

    /// <summary>
    /// Sets the penguin type for player 2
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP2(string penguinType)
    {
        switch (penguinType)
        {
            case "Hoiho":
                GameSettings.Player2Penguin = PenguinType.Hoiho;
                break;
            case "Kororā":
                GameSettings.Player2Penguin = PenguinType.Kororā;
                break;
            case "Tawaki":
                GameSettings.Player2Penguin = PenguinType.Tawaki;
                break;
            default:
                GameSettings.Player2Penguin = PenguinType.None;
                break;
        }
    }

    /// <summary>
    /// Toggles the player 2 exists boolean
    /// </summary>
    public void TogglePlayer2(GameObject player2Button)
    {
        GameSettings.Player2Exists = !GameSettings.Player2Exists;
        if (!GameSettings.Player2Exists)
        {
            player2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Add Player 2";
            player2Select.SetActive(false);
        }
        else
        {
            player2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Remove Player 2";
            player2Select.SetActive(true);
        }
    }

    /// <summary>
    /// Starts game with the currently selected level
    /// </summary>
    public void StartGame()
    {
        if (GameSettings.SelectedLevel == Levels.None)
        {
            Debug.LogError("No level selected");
            return;
        }
        SceneManager.LoadScene(GameSettings.SelectedLevel.ToString());
    }
}
