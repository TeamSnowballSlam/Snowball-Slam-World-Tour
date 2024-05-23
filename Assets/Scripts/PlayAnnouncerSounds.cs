using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnnouncerSounds : MonoBehaviour
{
    public PlaySFX playSFX;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayAnnouncerSounds Start");
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
        yield return new WaitForSeconds(1);
        playSFX.playSound("321GO");
    }

    //public void 
    //Have this get called to play the end game scripts
    //Public reg method that calls priv IEnumerator
}
