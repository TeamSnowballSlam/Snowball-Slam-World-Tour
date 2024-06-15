/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is used to play the announcer sounds at the start of the game and at the end of the game.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnnouncerSounds : MonoBehaviour
{
    public PlaySFX playSFX;
    // Start is called before the first frame update
    public static PlayAnnouncerSounds Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playSFX = GetComponent<PlaySFX>();
        switch (GameSettings.SelectedLevel)
        {
            case Levels.Australia:
                StartCoroutine(AustraliaIntro());
                break;
            case Levels.Tutorial:
                //In case we need something here
                break;
            default:
                //StartCoroutine(AustraliaIntro()); Can be used for testing
                break;
        }
    }

    /// <summary>
    /// This coroutine is used to play the intro sounds for the Australia level.
    /// </summary>
    private IEnumerator AustraliaIntro()
    {
        playSFX.playSound("WelcomeToAustralia");
        yield return new WaitForSeconds(1.5f);
        playSFX.playSound("321GO");
    }

    /// <summary>
    /// This coroutine is used to play the intro sounds for the Tutorial level.
    /// </summary>
    public void FinalCountdown()
    {
        StartCoroutine(EndGame());
    }

    /// <summary>
    /// This coroutine is used to play the end game sounds for the Tutorial level.
    /// </summary>
    private IEnumerator EndGame()
    {
        playSFX.playSound("54321");
        yield return new WaitForSeconds(5f);
        playSFX.playSound("GameOver");
    }

    /// <summary>
    /// This method is used to play the victory sound.
    /// </summary>
    public void PlayVictory()
    {
        playSFX.playSound("PenguinsWin");
    }

    /// <summary>
    /// This method is used to play the defeat sound.
    /// </summary>
    public void PlayDefeat()
    {
        playSFX.playSound("KangaroosWin");
    }

    /// <summary>
    /// This method is used to play the draw sound.
    /// </summary>
    public void PlayDraw()
    {
        playSFX.playSound("Draw");
    }
}
