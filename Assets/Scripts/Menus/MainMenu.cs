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

    //Something here for only allowing later levels to be selected when they are unlocked?
    
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
