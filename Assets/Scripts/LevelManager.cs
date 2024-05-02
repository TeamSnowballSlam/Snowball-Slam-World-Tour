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
    private int delayTime = 10; //The delay time before the level starts

    [SerializeField]
    private int targetScore = 15; //The score needed to win the level

    [Header("Colors")]
    public Color mediumColor;
    public Color criticalColor;
    

    private TextMeshProUGUI playerScoreText;
    private TextMeshProUGUI playerScoreTitle;
    private TextMeshProUGUI enemyScoreText;
    private TextMeshProUGUI enemyScoreTitle;

    private TextMeshProUGUI timerText;
    private TextMeshProUGUI timerTitle;

    private float currentTime;

    private Teams playerTeam = Teams.Penguins; //Which team the player is on

    [SerializeField]
    private Teams enemyTeam = Teams.Kangaroos; //Which team the enemy is on

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
        GameSettings.currentGameState = GameStates.PreGame;
        //Initialize the text objects
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        enemyScoreText = GameObject.Find("EnemyScore").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();

        playerScoreTitle = GameObject.Find("PlayerScoreTitle").GetComponent<TextMeshProUGUI>();
        enemyScoreTitle = GameObject.Find("EnemyScoreTitle").GetComponent<TextMeshProUGUI>();
        timerTitle = GameObject.Find("LevelTimerTitle").GetComponent<TextMeshProUGUI>();

        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
        playerScoreTitle.text = GetTeamName(playerTeam);
        enemyScoreTitle.text = GetTeamName(enemyTeam);

        timerTitle.text = "Starting in ";
        timerText.text = delayTime.ToString();
        currentTime = Time.time;
    }

    void Update()
    {
        {
            //Update the timer every second if the round is not over
            if (Time.time > currentTime + 1 && (GameSettings.currentGameState == GameStates.PreGame || GameSettings.currentGameState == GameStates.InGame))
            {
                if (delayTime > 0)
                {
                    currentTime = Time.time;
                    delayTime -= 1;
                    timerTitle.text = "Starting in ";
                    timerText.text = delayTime.ToString();
                }
                else
                {
                    // if (!roundStarted)
                    // {
                        StartCountdown();
                    // }
                    if (
                        secondsRemaining > 0
                        && playerScore < targetScore
                        && enemyScore < targetScore && GameSettings.currentGameState == GameStates.InGame /*!roundOver && roundStarted*/
                    )
                    {
                        currentTime = Time.time;
                        secondsRemaining -= 1;
                        timerTitle.text = "Time Remaining";
                        //Format the time to be displayed as MM:SS
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
                        DisplayWinner(CheckForWinner());
                        GameSettings.currentGameState = GameStates.PostGame;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method will return the name of the team based on the enum value
    /// </summary>
    /// <param name="team">The Team to be returned as a string</param>
    /// <returns>The specified team as a string</returns>
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

    /// <summary>
    /// This method will display the winner of the match
    /// </summary>
    /// <param name="winner">The team that won the match</param>
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

    /// <summary>
    /// This method will check for the winner of the match
    /// </summary>
    /// <returns>The winner as a string</returns>

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
        GameSettings.currentGameState = GameStates.InGame;
    }

    /// <summary>
    /// This method will update the score of the team that scored
    /// </summary>
    /// <param name="team">The name of the team which will be given points</param>
    public void UpdateScore(string team)
    {
        if (GameSettings.currentGameState != GameStates.InGame || playerScore == targetScore || enemyScore == targetScore)
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
