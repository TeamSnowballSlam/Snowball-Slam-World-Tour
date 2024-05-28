using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//attached to the player
public class PauseMenu : MonoBehaviour
{
    [SerializeField]private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if (GameSettings.currentGameState == GameStates.InGame)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameSettings.currentGameState = GameStates.Paused;
        }
        else if (GameSettings.currentGameState == GameStates.Paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GameSettings.currentGameState = GameStates.InGame;
        }
    }
}
