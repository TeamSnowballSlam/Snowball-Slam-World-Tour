using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To add a new song to the game, you need to add it to the Music folder in the Resources folder

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static MusicManager Instance;
    private AudioClip[] tracks; // The music tracks to play

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            OnMuteChanged();
            OnVolumeChanged();
            //Load music from resources
            tracks = Resources.LoadAll<AudioClip>("Music");
            SetTrack("Menu"); //This should always start in the menu
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Set the track to play
    /// </summary>
    /// <param name="track">Track to play</param>
    public void SetTrack(string track)
    {  
        foreach (AudioClip clip in tracks)
        {
            if (clip.name == track)
            {
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }
    }

    /// <summary>
    /// Change the volume of the music
    /// </summary>
    public void OnVolumeChanged()
    {
        audioSource.volume = GameSettings.MusicVolume;
    }

    /// <summary>
    /// Change if the music is muted
    /// </summary>
    public void OnMuteChanged()
    {
        audioSource.mute = GameSettings.Mute;
    }
}
