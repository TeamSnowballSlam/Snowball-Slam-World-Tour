using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle[] levelToggles; //Need to manually add each toggle in the inspector

    //Something here for only allowing later levels to be selected when they are unlocked?

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitButton()
    {
        // Quit the game
        Application.Quit();
    }

    /// <summary>
    /// Loads the character select menu
    /// </summary>
    public void CharacterSelect()
    {
        if (GameSettings.SelectedLevel != Levels.None)
        {
            SceneManager.LoadScene("CharacterSelect");
        }
        else
        {
            Debug.LogError("No level selected");
        }
    }

    /// <summary>
    /// Sets the selected level
    /// </summary>
    public void LevelSelect()
    {
        foreach (Toggle toggle in levelToggles)
        {
            if (toggle.isOn)
            {
                //This converts the string name of the toggle to the enum value and sets the selected level
                GameSettings.SelectedLevel = (Levels)System.Enum.Parse(typeof(Levels), toggle.name);
            }
        }
    }
}
