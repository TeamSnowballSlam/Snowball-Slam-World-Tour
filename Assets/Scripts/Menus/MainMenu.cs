using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject player2Select;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Opens the level select menu
    /// </summary>
    public void PlayButton()
    {
        // Go to level select
    }

    /// <summary>
    /// Opens the settings menu
    /// </summary>
    public void SettingsButton()
    {
        // Go to settings
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitButton()
    {
        // Quit the game
        Application.Quit();
    }
}
