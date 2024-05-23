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

    private IEnumerator AustraliaIntro()
    {
        playSFX.playSound("WelcomeToAustralia");
        yield return new WaitForSeconds(1.5f);
        playSFX.playSound("321GO");
    }

    public void FinalCountdown()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        playSFX.playSound("54321");
        yield return new WaitForSeconds(5f);
        playSFX.playSound("GameOver");
    }

    public void PlayVictory()
    {
        playSFX.playSound("PenguinsWin");
    }

    public void PlayDefeat()
    {
        playSFX.playSound("KangaroosWin");
    }

    public void PlayDraw()
    {
        playSFX.playSound("Draw");
    }
}
