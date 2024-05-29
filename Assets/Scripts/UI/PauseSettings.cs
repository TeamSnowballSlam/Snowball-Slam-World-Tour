using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    private float musicVolume;
    private float soundEffectsVolume;

    [SerializeField] private GameObject musicSlider; // The slider for the music volume
    [SerializeField] private GameObject soundEffectsSlider; // The slider for the sound effects volume
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private GameObject hudPause;
    [SerializeField] private GameObject hudHelp;
    [SerializeField] private GameObject P2Snowballs;

    void Start()
    {
        musicSlider.GetComponent<Slider>().value = GameSettings.MusicVolume;
        musicSlider.GetComponent<Slider>().value = GameSettings.SoundEffectsVolume;
        controls.SetActive(false);
    }

    /// <summary>
    /// When the music slider is changed
    /// </summary>
    /// <param name="value">Value being passed through from 0 to 1</param>
    public void OnMusicChanged(float value)
    {
        GameSettings.MusicVolume = value;
    }

    /// <summary>
    /// Mutes the music
    /// </summary>
    public void MuteMusic()
    {
        if (GameSettings.MusicVolume > 0) // If music is not muted
        {
            musicVolume = GameSettings.MusicVolume; // store the current volume
            GameSettings.MusicVolume = 0; // Mute the music
        }
        else // else if the music is at 0 (muted)
        {
            GameSettings.MusicVolume = musicVolume; // set the music back to the volume it was at
        }
        musicSlider.GetComponent<Slider>().value = GameSettings.MusicVolume; // Should change the slider value
    }

    /// <summary>
    /// When the sound effects slider is changed
    /// </summary>
    public void OnSoundEffectsChanged(float value)
    {
        GameSettings.SoundEffectsVolume = value;
    }

    /// <summary>
    /// Mutes the sound effects
    /// </summary>
    public void MuteSoundEffects()
    {
        if (GameSettings.SoundEffectsVolume > 0) // If sound effects are not muted
        {
            soundEffectsVolume = GameSettings.SoundEffectsVolume; // store the current volume
            GameSettings.SoundEffectsVolume = 0; // Mute the sound effects
        }
        else // else if the sound effects are at 0 (muted)
        {
            GameSettings.SoundEffectsVolume = soundEffectsVolume; // set the sound effects back to the volume it was at
        }
        soundEffectsSlider.GetComponent<Slider>().value = GameSettings.SoundEffectsVolume; // Should change the slider value
    }

    /// <summary>
    /// Exits to main menu
    /// </summary>
    public void MainMenu()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetTrack("Menu");
        }
        GameSettings.Player2Exists = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Hotjoin for adding player 2
    /// </summary>
    public void OnHotJoin()
    {
        if (GameSettings.Player2Exists)
        {
            return;
        }
        GameSettings.currentGameState = GameStates.InGame;
        Time.timeScale = 1;
        hudPause.SetActive(true);
        hudHelp.SetActive(true);
        P2Snowballs.SetActive(true);
        SpawnManager.Instance.spawnPlayerTwo();
    }

    /// <summary>
    /// Restarts the round
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
    }

    /// <summary>
    /// Shows the controls
    /// </summary>
    public void HelpButton()
    {
        if (controls.activeSelf)
        {
            if(!pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                GameSettings.currentGameState = GameStates.InGame;
            }
            pauseContainer.SetActive(true);
            controls.SetActive(false);
        }
        else
        {
            if (pauseMenu.activeSelf)
            {
                pauseContainer.SetActive(false);
            }
            Time.timeScale = 0;
            GameSettings.currentGameState = GameStates.Paused;
            controls.SetActive(true);
        }
    }

    /// <summary>
    /// Pauses the game. Same as on penguin but for the pause UI button
    /// </summary>
    public void PauseButton()
    {
        if (GameSettings.currentGameState == GameStates.InGame)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            GameSettings.currentGameState = GameStates.Paused;
            hudPause.SetActive(false);
            hudHelp.SetActive(false);
        }
        else if (GameSettings.currentGameState == GameStates.Paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            GameSettings.currentGameState = GameStates.InGame;
            hudPause.SetActive(true);
            hudHelp.SetActive(true);
        }
    }
}
