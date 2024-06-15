/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 01/05/2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is used to handle the main menu. It allows the player to select a level and start the game.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menus; //All menus to turn off at the start

    
    private void Start()
    {
        //Turn off all menus
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitButton()
    {
        // Quit the game
        Application.Quit();
    }

    /// <summary>
    /// Sets the selected level
    /// </summary>
    public void LevelSelect(string name)
    {
        //This converts the string name of the toggle to the enum value and sets the selected level
        GameSettings.SelectedLevel = (Levels)System.Enum.Parse(typeof(Levels), name);
    }
}
