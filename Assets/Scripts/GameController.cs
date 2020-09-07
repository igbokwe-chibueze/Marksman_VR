using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Texts")]

    [Tooltip("Displays the player's score")]
    public TextMeshProUGUI scoreText;

    [Tooltip("Displays the duration of the game")]
    public TextMeshProUGUI timerText;

    [Tooltip("Displays the games difficulty level")]
    public TextMeshProUGUI difficultyText;


    [Header("UI")]

    [Tooltip("Canvas holding the GameOver UI")]
    public GameObject gameOverScreen;

    [Tooltip("Canvas holding the Main Menu UI")]
    public GameObject titleScreen;
    

    [Header("Spawning")]

    [Tooltip("Drones to spawn")]
    public List<GameObject> targetPrefabs;

    [Tooltip("Where to spawn drones")]
    public Transform[] spawnPoints;

    [Tooltip("How often the spawning of drones occurs. This value is multiplied by the game difficulty")]
    private float spawnRate = 0.5f;

    [Header("Others")]

    [Tooltip("The audiosource holding the games theme")]
    public GameObject audioPlayer;

    [Tooltip("The duration of the game")]
    public float startTimer = 60f;

    //Player's score
    private int score;

    //Counts down the StartTimer.
    private float timer;

    //Difficulty of the game.
    [HideInInspector] public int playDifficulty;
    
    //Has the game started or not.
    [HideInInspector]public bool isGameActive;

    
    
    private void Start() 
    {
        gameOverScreen.SetActive(false);
        audioPlayer.SetActive(true);
    }
    
    //Start the game.
    public void StartGame(int difficulty)
    {
        if (difficulty == 1)
        {
            difficultyText.text = "Easy";
        }else if (difficulty == 2)
            {
                difficultyText.text = "Meduim";
            }else if (difficulty == 3)
                {
                    difficultyText.text = "Hard";
                }

        playDifficulty = difficulty;

        //spawnRate /= difficulty;
        spawnRate *= difficulty;
        isGameActive = true;
        timer = startTimer;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);

        titleScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = "Timer: " + Mathf.Round(timer);
            }else if (timer <= 0)
                {
                    GameOver();
                }
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            int randomSpawnPoints = Random.Range (0, spawnPoints.Length);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], spawnPoints [randomSpawnPoints].position, targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "score: " + score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
        audioPlayer.SetActive(false);
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Exit game by reloading the scene
    public void ExitGame()
    {
        Application.Quit();
    }
}
