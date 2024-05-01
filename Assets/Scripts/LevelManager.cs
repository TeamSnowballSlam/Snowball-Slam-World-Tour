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
        string formattedTime = string.Format("{0:00}:{1:00}", secondsRemaining / 60, secondsRemaining % 60);
        timerText.text = formattedTime;
        currentTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
