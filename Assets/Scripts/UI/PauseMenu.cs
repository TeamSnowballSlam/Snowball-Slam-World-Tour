/// <remarks>
/// Author: Erika Stuart
/// Date Created: 27/05/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This script manages the pause menu.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//attached to the player
public class PauseMenu : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Pauses the game and displays the pause menu.
    /// </summary>
    public void PauseGame()
    {
        if (GameSettings.currentGameState == GameStates.InGame)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameSettings.currentGameState = GameStates.Paused;
            EventSystem.current.SetSelectedGameObject(PauseSettings.Instance.pauseSelectable);
            PauseSettings.Instance.pauseAnimator.SetTrigger("pointerEnter");
        }
        else if (GameSettings.currentGameState == GameStates.Paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GameSettings.currentGameState = GameStates.InGame;
        }
    }

    /// <summary>
    /// Quit  the game.
    /// </summary>
    public void ForceQuit()
    {
        Application.Quit();
    }
}
