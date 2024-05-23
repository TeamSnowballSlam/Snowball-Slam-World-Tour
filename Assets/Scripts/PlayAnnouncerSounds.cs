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
        Debug.Log("Australia Intro");
        playSFX.playSound("WelcomeToAustralia");
        yield return new WaitForSeconds(1.5f);
        playSFX.playSound("321GO");
    }

    public void FinalCountdown()
    {
        playSFX.playSound("54321");
    }
}
