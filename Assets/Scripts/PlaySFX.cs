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
    public void StopSound()
    {
        audioSource.Stop();
    }
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
