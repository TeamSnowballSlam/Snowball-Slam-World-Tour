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

    public Color mediumColor;
    public Color criticalColor;
    private bool roundOver = false;
    private bool timerStarted = false;

    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI enemyScoreText;
    private TextMeshProUGUI timerText;
    private float currentTime;
   
    public static LevelManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                    if(secondsRemaining <= 5)
                    {
                        timerText.color = criticalColor;
                    }
                    else if(secondsRemaining <= 10)
                    {
                        timerText.color = mediumColor;
                    }
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

    public void UpdateScore(string team)
    {
        if(team == "Player")
        {
            currentScore += 1;
            playerScoreText.text = currentScore.ToString();
        }
        else if(team == "Enemy")
        {
            enemyScore += 1;
            enemyScoreText.text = enemyScore.ToString();
        }
        else return;
    }
    


}
