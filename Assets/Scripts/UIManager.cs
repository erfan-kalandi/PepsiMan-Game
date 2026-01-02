using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * This script manages the game's User Interface, including real-time score calculation 
 * based on player distance, high score tracking via PlayerPrefs, and handling the Game Over screen.
 */
public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverPanel;
    public Transform player;

    private int score;

    void Start()
    {
        highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            score = (int)player.position.z;
            scoreText.text = "Score: " + score;

            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                highScoreText.text = "Best: " + score.ToString();
            }
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        int lastHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > lastHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}