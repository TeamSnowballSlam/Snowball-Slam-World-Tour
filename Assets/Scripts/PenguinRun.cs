using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinRun : MonoBehaviour
{
    PlaySFX playSFX;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playSFX = GetComponent<PlaySFX>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.TouchingRoad())
        { 
            if (!playSFX.GetSoundName().Equals("HotRoad"))
            {
                playSFX.playSound("HotRoad");
            }
        }
        else if (playerMovement.IsSliding)
        {
            if (!playSFX.GetSoundName().Equals("Slide"))
            {
                playSFX.playSound("Slide");
            }
        }
        else if (playerMovement.IsMoving)
        {
            if (!playSFX.GetSoundName().Equals("Run"))
            {
                playSFX.playSound("Run");
            }
        }
        else
        {
            playSFX.StopSound();
        }
    }
}
