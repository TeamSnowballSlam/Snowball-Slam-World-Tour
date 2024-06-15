/// <remarks>
/// Author: Palin
/// Date Created: 01/05/2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is used to handle the character select screen. It allows the player to select their penguin and start the game.
/// </summary>
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
    public Selectable JoinButton;
    // Start is called before the first frame update

    /// <summary>
    /// Sets the penguin type for player 1
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP1()
    {
        GameSettings.Player1Penguin = penguinSwitch(findPenguins(Player1Penguins));
        Debug.Log(GameSettings.Player1Penguin);
    }

    /// <summary>
    /// Sets the penguin type for player 2
    /// </summary>
    /// <param name="penguinType">Penguin selected</param>
    public void SetPenguinP2()
    {
        GameSettings.Player2Penguin = penguinSwitch(findPenguins(Player2Penguins));
        Debug.Log(GameSettings.Player2Penguin);
    }
    
    /// <summary>
    /// Finds the penguin that is currently active
    /// </summary>
    /// <param name="penguins">Array of penguins</param>
    /// <returns>String of the penguin name</returns>
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

    /// <summary>
    /// Switch statement to return the penguin type
    /// </summary>
    /// <param name="penguinType">String of the penguin name</param>
    /// <returns>The PenguinType</returns>
    private PenguinType penguinSwitch(string penguinType)
    {
        Debug.Log(penguinType);
        switch (penguinType)
        {
            case "Hoiho":
                return PenguinType.Hoiho;
            case "Kororā":
            case "Kororaa":
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

    /// <summary>
    /// Changes the selectable object for keyboard navigation when the join button is pressed
    /// </summary>
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
    /// Changes the selectable object for keyboard navigation when the join button is pressed
    /// </summary>
    public void ChangeBackSelectable(Selectable selectable)
    {
        try
        {
            Navigation navigation = selectable.navigation;
            navigation.selectOnRight = JoinButton;
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
        SetPenguinP1();
        if (GameSettings.Player2Exists)
        {
            SetPenguinP2();
        }
        if (GameSettings.SelectedLevel == Levels.None)
        {
            Debug.LogError("No level selected");
            return;
        }
        SceneManager.LoadScene(GameSettings.SelectedLevel.ToString());
    }

    /// <summary>
    /// Resets the player penguins to none
    /// </summary>
    public void BackToLevel()
    {
        GameSettings.Player2Exists = false;
        GameSettings.Player1Penguin = PenguinType.None;
        GameSettings.Player2Penguin = PenguinType.None;
    }
}
