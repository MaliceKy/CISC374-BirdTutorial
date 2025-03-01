using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject startScreen;
    public AudioSource scoreSFX;
    public TextMeshProUGUI highScoreText;
    private int highScore;
    public Color scoreHighlightColor = Color.yellow; // Color to change to when scoring
    private Color originalColor;

    void Start(){
        // Load the saved high score when the game starts
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (startScreen != null)
        {
            startScreen.SetActive(true);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

         if (scoreText != null)
        {
            originalColor = scoreText.color;
        }
        
        // Pause the game until player presses Start
        Time.timeScale = 0;
    }

[ContextMenu("Increase Score")]
   public void addScore(int scoreToAdd)
   {
    playerScore = playerScore + scoreToAdd;
    scoreText.text = playerScore.ToString();

     if (scoreSFX != null)
        {
            scoreSFX.Play();
        }
        else
        {
            Debug.LogWarning("scoreSFX is not assigned!");
        }

        StartCoroutine(HighlightScore());
   }

    private System.Collections.IEnumerator HighlightScore()
    {
        if (scoreText != null)
        {
            scoreText.color = scoreHighlightColor; 
            yield return new WaitForSeconds(0.2f); 
            scoreText.color = originalColor;
        }
    }

   public void StartGame()
    {
        // Hide start screen
        if (startScreen != null)
        {
            startScreen.SetActive(false);
        }
        
        // Reset score
        playerScore = 0;
        if (scoreText != null)
        {
            scoreText.text = "0";
        }
        
        // Resume game
        Time.timeScale = 1;
    }

   public void RestartGame(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void gameOver(){
    // Check if current score is higher than stored high score
    if (playerScore > highScore)
    {
        // Update high score
        highScore = playerScore;
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
    
    // Always update high score text, regardless of whether it's a new high score
    UpdateHighScoreText();
    
    // Show game over screen
    gameOverScreen.SetActive(true);
}

   private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }

}
