/// <remarks>
/// Author: Palin Wiseman 
/// Date Created: 11/04/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// Static class to store game settings.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static PenguinType Player1Penguin = PenguinType.None; // The penguin type for player 1
    public static PenguinType Player2Penguin = PenguinType.None; // The penguin type for player 2
    public static bool Player2Exists = false; // If player 2 exists. This is set in character select or when player 2 hot joins
    public static Levels SelectedLevel = Levels.None; // The level selected for the game
    public static GameStates currentGameState = GameStates.PreGame; // The current state of the game
    private static float musicVolume; // The volume of the music
    public static float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.OnVolumeChanged();
            }
        }
    }
    private static float soundEffectsVolume; // The volume of the sound effects
    public static float SoundEffectsVolume
    {
        get
        {
            return soundEffectsVolume;
        }
        set
        {
            soundEffectsVolume = value;
            PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsVolume);
        }
    }
    private static bool mute = false; // If the game is muted
    public static bool Mute
    {
        get
        {
            return mute;
        }
        set
        {
            mute = value;
            PlayerPrefs.SetInt("Mute", mute ? 1 : 0);
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.OnMuteChanged();
            }
        }
    }
}
