using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//attached to the player
public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        //pauseMenu.transform.Find("MusicVol").GetComponent<Slider>().value = GameSettings.MusicVolume;
        pauseMenu.transform.GetChild(0).Find("MusicVol").GetComponent<Slider>().value = GameSettings.MusicVolume;
        //pauseMenu.transform.Find("SFXVol").GetComponent<Slider>().value = GameSettings.SoundEffectsVolume;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        if (pauseMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        isPaused = !isPaused;
    }
}
