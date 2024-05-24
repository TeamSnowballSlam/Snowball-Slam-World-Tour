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
