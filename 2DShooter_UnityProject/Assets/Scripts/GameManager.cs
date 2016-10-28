using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Source: GameManager.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Singleton class that manages the game
 */

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int scoreToFaceTheBoss = 500;
    int totalScore = 0;
    int lastScore = 0;

    public Text scoreText;  // the score Text UI element
    public GameObject gameOverScreen;
    public GameObject boss;
    public Spawner enemySpawner;
    public Spawner bonusSpawner;
    public AudioClip bossTheme;
    public AudioClip mainTheme;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update checks for 'R' press in order to reload the game
    /// </summary>
    void Update() {
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }
	
    /// <summary>
    /// Add points to the player score
    /// </summary>
    /// <param name="score"></param>
	public void AddScore(int points) {
        lastScore = totalScore % scoreToFaceTheBoss;
        totalScore += points;
        scoreText.text = "Score: " + totalScore;
        if(totalScore % scoreToFaceTheBoss < lastScore) {
            audioSource.clip = bossTheme;
            audioSource.Play();
            Instantiate(boss).SetActive(true);
            enemySpawner.StopSpawn();
            bonusSpawner.StopSpawn();
        }
    }

    /// <summary>
    /// Calls the game over screen
    /// </summary>
    public void GameOver() {
        gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Called when the boss is defeated
    /// </summary>
    public void BossDefeated() {
        audioSource.clip = mainTheme;
        audioSource.Play();
        enemySpawner.StartSpawn();
        bonusSpawner.StartSpawn();
    }
}
