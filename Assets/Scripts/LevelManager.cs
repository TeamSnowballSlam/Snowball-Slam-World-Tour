/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: May 2, 2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class manages the level, it will keep track of each teams score , the time ramaining and the current state of the level
/// </summary>


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level Management")]
    [SerializeField]
    private int playerScore = 0;

    [SerializeField]
    private int enemyScore = 0;

    [SerializeField]
    private int secondsRemaining = 60; //The time remaining in the level



    [SerializeField]
    private int targetScore = 15; //The score needed to win the level

    [Header("Colors")]
    public Color mediumColor;
    public Color criticalColor;
    private bool roundOver = false;
    private bool timerStarted = false;

    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI playerScoreTitle;
    private TextMeshProUGUI enemyScoreText;
    private TextMeshProUGUI enemyScoreTitle;

    private TextMeshProUGUI timerText;

    private float currentTime;

    private Teams playerTeam = Teams.Penguins;
    [SerializeField] private Teams enemyTeam = Teams.Kangaroos;

    public static LevelManager instance;

    void Awake()
    {
        if (instance == null)
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

        playerScoreTitle = GameObject.Find("PlayerScoreTitle").GetComponent<TextMeshProUGUI>();
        enemyScoreTitle = GameObject.Find("EnemyScoreTitle").GetComponent<TextMeshProUGUI>();

        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
        playerScoreTitle.text = GetTeamName(playerTeam);
        enemyScoreTitle.text = GetTeamName(enemyTeam);

        //Format the time to display as MM:SS
        string formattedTime = string.Format(
            "{0:00}:{1:00}",
            secondsRemaining / 60,
            secondsRemaining % 60
        );
        timerText.text = formattedTime;
        currentTime = Time.time;
    }

    void Update()
    {
        if (!timerStarted)
        {
            StartCountdown();
        }
        else
        {
            //Update the timer every second if the round is not over
            if (Time.time > currentTime + 1 && !roundOver)
            {
                if (secondsRemaining > 0 && playerScore < targetScore && enemyScore < targetScore)
                {
                    currentTime = Time.time;
                    secondsRemaining -= 1;
                    string formattedTime = string.Format(
                        "{0:00}:{1:00}",
                        secondsRemaining / 60,
                        secondsRemaining % 60
                    );
                    timerText.text = formattedTime;

                    //Once the timer reaches either threshold, change the color of the text
                    if (secondsRemaining <= 5)
                    {
                        timerText.color = criticalColor;
                    }
                    else if (secondsRemaining <= 10)
                    {
                        timerText.color = mediumColor;
                    }
                }
                else
                {
                    roundOver = true;
                    DisplayWinner(CheckForWinner());
                }
            }
        }
    }

    private string GetTeamName(Teams team)
    {
        if (team == Teams.Penguins)
        {
            return "Penguins";
        }
        else if (team == Teams.Kangaroos)
        {
            return "Kangaroos";
        }
        else if (team == Teams.RedPandas)
        {
            return "Red Pandas";
        }
        else if (team == Teams.Capybaras)
        {
            return "Capybaras";
        }
        else
        {
            return "Unknown";
        }
    }

    private void DisplayWinner(string winner)
    {
        if (winner == "Player")
        {
            timerText.text = "Penguins Wins!";
        }
        else if (winner == "Enemy")
        {
            timerText.text = "Penguins Lost!";
        }
        else
        {
            timerText.text = "Draw!";
        }
    }

    private string CheckForWinner()
    {
        if (secondsRemaining <= 0)
        {
            if (playerScore > enemyScore)
            {
                return "Player";
            }
            else if (enemyScore > playerScore)
            {
                return "Enemy";
            }
            else
            {
                return "Draw";
            }
        }
        else
        {
            if (playerScore >= targetScore)
            {
                return "Player";
            }
            else if (enemyScore >= targetScore)
            {
                return "Enemy";
            }
            else
            {
                return "Draw";
            }
        }
    }

    /// <summary>
    /// This method will start the countdown timer
    /// </summary>
    private void StartCountdown()
    {
        timerStarted = true;
    }

    /// <summary>
    /// This method will update the score of the team that scored
    /// </summary>
    /// <param name="team">The name of the team which will be given points</param>
    public void UpdateScore(string team)
    {
        if (roundOver)
            return;
        if (team == "Player")
        {
            playerScore += 1;
            playerScoreText.text = playerScore.ToString();
        }
        else if (team == "Enemy")
        {
            enemyScore += 1;
            enemyScoreText.text = enemyScore.ToString();
        }
        else
            return;
    }
}
