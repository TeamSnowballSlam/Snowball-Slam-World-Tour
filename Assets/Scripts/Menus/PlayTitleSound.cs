/// <remarks>
/// Author: Palin Wiseman
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is used to play the title sound when the game starts.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTitleSound : MonoBehaviour
{
    public PlaySFX playSFX;
    // Start is called before the first frame update
    void Start()
    {
        playSFX.playSound("SnowballSlamWorldTour");
    }
}
