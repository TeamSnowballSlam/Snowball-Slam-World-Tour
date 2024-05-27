using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettings : MonoBehaviour
{
    /// <summary>
    /// When the music slider is changed
    /// </summary>
    /// <param name="value">Value being passed through from 0 to 1</param>
    public void OnMusicChanged(float value)
    {
        GameSettings.MusicVolume = value;
    }

    public void OnSoundEffectsChanged(float value)
    {
        GameSettings.SoundEffectsVolume = value;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OnHotJoin()
    {
        Time.timeScale = 1;
    }

}
