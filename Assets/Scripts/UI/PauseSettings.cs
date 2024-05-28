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

    void Start()
    {
        musicSlider.GetComponent<Slider>().value = GameSettings.MusicVolume;
        musicSlider.GetComponent<Slider>().value = GameSettings.SoundEffectsVolume;
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
