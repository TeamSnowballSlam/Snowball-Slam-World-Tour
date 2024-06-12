/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This class is used to play sound effects in the game.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// This method is used to play a sound effect.
    /// </summary>
    public void playSound(string sound)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.mute = GameSettings.Mute;
        audioSource.volume = GameSettings.SoundEffectsVolume;
        AudioClip clip = Resources.Load<AudioClip>("SoundEffects/" + sound);
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// This method is used to stop the sound that is currently playing.
    /// </summary>
    public void StopSound()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// This method is used to get the name of the sound that is currently playing.
    /// </summary>
    public string GetSoundName()
    {
        try
        {
            return audioSource.clip.name;
        }
        catch
        {
            return "";
        }
    }
}
