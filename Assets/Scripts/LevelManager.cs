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
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Note from Palin: I went through and hid the values that don't need to be shown in the inspector anymore
    //I also changed the values to the ones we have decided on
    [Header("Level Management")]
    private int playerScore = 0;
    private int enemyScore = 0;
    [HideInInspector]
    public int LevelLength = 90; //How long the level will last in seconds
    [HideInInspector]
    public int secondsRemaining; //The time remaining in the level

    private float delayTime = 5; //The delay time before the level starts

    private int targetScore = 15; //The score needed to win the level

    [Header("Colors")]
    public Color mediumColor;
    public Color criticalColor;

    public List<Transform> endGameWinnerSpawnPoints = new List<Transform>();
    public List<Transform> endGameLoserSpawnPoints = new List<Transform>();
    public List<Transform> endGameDrawSpawnPoints = new List<Transform>();

    public GameObject mainCamera;
    public GameObject endGameCamera;
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
    public GameObject restartButton;
    public GameObject continueButton;
    //public GameObject addPlayerButton;

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
        restartButton.SetActive(false);
        continueButton.SetActive(false);
        mainCamera.SetActive(true);
        endGameCamera.SetActive(false);
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
        secondsRemaining = LevelLength;
    }

    void Update()
    {
        {
            //Update the timer every second if the round is not over
            if (
                Time.time > currentTime + 1
                && (
                    GameSettings.currentGameState == GameStates.PreGame
                    || GameSettings.currentGameState == GameStates.InGame
                )
            )
            {
                if (currentTime == 5) { PlayAnnouncerSounds.Instance.FinalCountdown(); }
                if (delayTime > 0)
                {
                    currentTime = Time.time;
                    delayTime -= 1;
                    timerTitle.text = "Starting in ";
                    timerText.text = delayTime.ToString();
                }
                else
                {
                    StartCountdown();

                    if (
                        secondsRemaining > 0
                        && playerScore < targetScore
                        && enemyScore < targetScore
                        && GameSettings.currentGameState == GameStates.InGame /*!roundOver && roundStarted*/
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
        switch (team)
        {
            case Teams.Penguins:
                return "Penguins";
            case Teams.Kangaroos:
                return "Kangaroos";
            case Teams.RedPandas:
                return "Red Pandas";
            case Teams.Capybaras:
                return "Capybaras";
            default:
                return "Unknown";
        }
    }

    /// <summary>
    /// This method will display the winner of the match
    /// </summary>
    /// <param name="winner">The team that won the match</param>
    private void DisplayWinner(string winner)
    {
        mainCamera.SetActive(false);
        endGameCamera.SetActive(true);
        //addPlayerButton.SetActive(false);

        GameObject[] snowballs = GameObject.FindGameObjectsWithTag("Snowball");
        foreach (GameObject snowball in snowballs)
        {
            Destroy(snowball);
        }
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            Destroy(turret);
        }
        if (winner == "Player")
        {
            PlayAnnouncerSounds.Instance.PlayVictory();
            MusicManager.Instance.SetTrack("Win");
            restartButton.SetActive(false);
            continueButton.SetActive(true);
            timerText.text = "Penguins Win!";
            timerText.color = Color.green;
            GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];

            p1.transform.parent = endGameWinnerSpawnPoints[0];
            p1.transform.localPosition = Vector3.zero;
            p1.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                GameObject p2 = GameObject.FindGameObjectsWithTag("Player")[1];
                p2.transform.parent = endGameWinnerSpawnPoints[1];
                p2.transform.localPosition = Vector3.zero;
                p2.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            }
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                GameObject e = GameObject.FindGameObjectsWithTag("Enemy")[i];
                e.transform.parent = endGameLoserSpawnPoints[i];
                e.transform.localPosition = Vector3.zero;
                e.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

                e.GetComponent<NavMeshAgent>().isStopped = true;
                e.GetComponent<NavMeshAgent>().SetDestination(e.transform.position);
                e.GetComponent<NavMeshAgent>().enabled = false;
                e.GetComponent<EnemyMovement>().enabled = false;
            }
        }
        else if (winner == "Enemy")
        {
            PlayAnnouncerSounds.Instance.PlayDefeat();
            MusicManager.Instance.SetTrack("Lose");
            restartButton.SetActive(true);
            continueButton.SetActive(true);
            GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];
            p1.transform.parent = endGameLoserSpawnPoints[0];
            p1.transform.localPosition = Vector3.zero;
            p1.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                GameObject p2 = GameObject.FindGameObjectsWithTag("Player")[1];
                p2.transform.parent = endGameLoserSpawnPoints[1];
                p2.transform.localPosition = Vector3.zero;
                p2.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                GameObject e = GameObject.FindGameObjectsWithTag("Enemy")[i];
                e.transform.parent = endGameWinnerSpawnPoints[i];
                e.transform.localPosition = Vector3.zero;
                e.transform.localRotation = Quaternion.Euler(Vector3.zero);
                e.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

                e.GetComponent<NavMeshAgent>().SetDestination(e.transform.localPosition);
                e.GetComponent<NavMeshAgent>().isStopped = true;
                e.GetComponent<NavMeshAgent>().enabled = false;
                e.GetComponent<EnemyMovement>().enabled = false;
            }
            timerText.text = "Penguins Lost!";
            timerText.color = criticalColor;
        }
        else
        {
            PlayAnnouncerSounds.Instance.PlayDraw();
            MusicManager.Instance.SetTrack("Draw");
            restartButton.SetActive(true);
            continueButton.SetActive(true);
            timerText.text = "Draw!";
            timerText.color = Color.white;
            GameObject p1 = GameObject.FindGameObjectsWithTag("Player")[0];

            p1.transform.parent = endGameDrawSpawnPoints[1];
            p1.transform.localPosition = Vector3.zero;
            p1.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                GameObject p2 = GameObject.FindGameObjectsWithTag("Player")[1];
                p2.transform.parent = endGameDrawSpawnPoints[0];
                p2.transform.localPosition = Vector3.zero;
                p2.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                GameObject e = GameObject.FindGameObjectsWithTag("Enemy")[i];
                e.transform.parent = endGameDrawSpawnPoints[i + 2];
                e.transform.localPosition = Vector3.zero;
                e.transform.localRotation = Quaternion.Euler(new(0, 180, 0));

                e.GetComponent<NavMeshAgent>().SetDestination(e.transform.localPosition);
                e.GetComponent<NavMeshAgent>().isStopped = true;
                e.GetComponent<NavMeshAgent>().enabled = false;
                e.GetComponent<EnemyMovement>().enabled = false;

            }
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
        if (
            GameSettings.currentGameState != GameStates.InGame
            || playerScore == targetScore
            || enemyScore == targetScore
        )
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

    public void RestartLevel()
    {
        MusicManager.Instance.SetTrack("Fight");
        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Continue()
    {
        MusicManager.Instance.SetTrack("Menu");
        GameSettings.Player2Exists = false;
        SceneManager.LoadScene("MainMenu"); //Commented out until we merge
    }
}
