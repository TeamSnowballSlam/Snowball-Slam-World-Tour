using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSettings : MonoBehaviour
{
    private float musicVolume;
    private float soundEffectsVolume;

    /// <summary>
    /// When the music slider is changed
    /// </summary>
    /// <param name="value">Value being passed through from 0 to 1</param>
    public void OnMusicChanged(float value)
    {
        GameSettings.MusicVolume = value;
        musicVolume = value;
    }

    /// <summary>
    /// Mutes the music
    /// </summary>
    public void MuteMusic()
    {
        if (GameSettings.MusicVolume > 0)
        {
            GameSettings.MusicVolume = 0;
        }
        else
        {
            GameSettings.MusicVolume = musicVolume;
        }
    }

    /// <summary>
    /// When the sound effects slider is changed
    /// </summary>
    public void OnSoundEffectsChanged(float value)
    {
        GameSettings.SoundEffectsVolume = value;
        soundEffectsVolume = value;
    }

    /// <summary>
    /// Mutes the sound effects
    /// </summary>
    public void MuteSoundEffects()
    {
        if (GameSettings.SoundEffectsVolume > 0)
        {
            GameSettings.SoundEffectsVolume = 0;
        }
        else
        {
            GameSettings.SoundEffectsVolume = soundEffectsVolume;
        }
    }

    /// <summary>
    /// Exits to main menu
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1;
       SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Hotjoin for adding player 2
    /// </summary>
    public void OnHotJoin()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Restarts the round
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
    }


}
