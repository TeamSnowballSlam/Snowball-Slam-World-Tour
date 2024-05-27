using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
