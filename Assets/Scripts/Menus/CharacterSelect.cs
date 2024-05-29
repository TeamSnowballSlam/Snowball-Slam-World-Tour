using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] Player1Penguins;
    public GameObject[] Player2Penguins;
    public Selectable LeftArrow;
    // Start is called before the first frame update

    /// <summary>
    /// Sets the penguin type for player 1
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP1()
    {
        GameSettings.Player1Penguin = penguinSwitch(findPenguins(Player1Penguins));
    }

    /// <summary>
    /// Sets the penguin type for player 2
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP2()
    {
        GameSettings.Player2Penguin = penguinSwitch(findPenguins(Player2Penguins));
    }
    
    private string findPenguins(GameObject[] penguins)
    {
        foreach (GameObject penguin in penguins)
        {
            if (penguin.activeSelf)
            {
                return penguin.name;
            }
        }
        return "None";
    }

    private PenguinType penguinSwitch(string penguinType)
    {
        switch (penguinType)
        {
            case "Hoiho":
                return PenguinType.Hoiho;
            case "Kororā":
                return PenguinType.Kororā;
            case "Tawaki":
                return PenguinType.Tawaki;
            default:
                return PenguinType.None;
        }
    }

    /// <summary>
    /// Toggles the player 2 exists boolean
    /// </summary>
    public void AddPlayer2()
    {
        GameSettings.Player2Exists = true;
    }

    public void ChangeSelectable(Selectable selectable)
    {
        try
        {
            Navigation navigation = selectable.navigation;
            navigation.selectOnRight = LeftArrow;
            selectable.navigation = navigation;
        }
        catch
        {
            Debug.LogError("Selectable not found");
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
    public void BackToLevel()
    {
        GameSettings.Player2Exists = false;
        GameSettings.Player1Penguin = PenguinType.None;
        GameSettings.Player2Penguin = PenguinType.None;
    }
}
