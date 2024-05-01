using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int currentScore = 0;
    [SerializeField] private int enemyScore = 0;
    [SerializeField] private int secondsRemaining = 60;
    private bool roundOver = false;
    private bool timerStarted = false;

    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI enemyScoreText;
    private TextMeshProUGUI timerText;
    private float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        enemyScoreText = GameObject.Find("EnemyScore").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();

        playerScoreText.text =  currentScore.ToString();
        enemyScoreText.text = enemyScore.ToString();

        //Format the time to display as MM:SS
        string formattedTime = string.Format("{0:00}:{1:00}", secondsRemaining / 60, secondsRemaining % 60);
        timerText.text = formattedTime;
        currentTime = Time.time;

    }

    void Update()
    {
        if(!timerStarted)
        {
            StartCountdown();
        }
        else
        {
            //Update the timer every second if the round is not over
            if (Time.time > currentTime + 1 && !roundOver)
            {
                if(secondsRemaining > 0)
                {
                    currentTime = Time.time;
                    secondsRemaining -= 1;
                    string formattedTime = string.Format("{0:00}:{1:00}", secondsRemaining / 60, secondsRemaining % 60);
                    timerText.text = formattedTime;
                }
                else
                {
                    roundOver = true;
                }
            }
        }
    }

    private void StartCountdown()
    {
        timerStarted = true;
    }


}
