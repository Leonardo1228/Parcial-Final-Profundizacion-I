using UnityEngine;
using UnityEngine.UI;

public class GameManagerP : MonoBehaviour
{
    public static GameManagerP Instance;

    public GameObject ballPrefab;
    public Text scoreText;

    private int RightScores = 0;
    private int LeftScores = 0;

    private Ball currentBall; // Referencia a la bola actual

    public GameObject retryButton;
    public GameObject mainMenuButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
        SpawnBall();
        retryButton.SetActive(false);
        mainMenuButton.SetActive(false);

    }

    void SpawnBall()
    {
        GameObject ballObj = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall = ballObj.GetComponent<Ball>();
    }

    public void AIScoresPoint()
    {
        RightScores++;
        UpdateScoreText();
        CheckWinCondition();
        ResetCurrentBall();
    }

    public void PlayerScoresPoint()
    {
        LeftScores++;
        UpdateScoreText();
        CheckWinCondition();
        ResetCurrentBall();
    }

    void ResetCurrentBall()
    {
        if (currentBall != null)
            currentBall.ResetBall();
    }

    void UpdateScoreText()
    {
        scoreText.text = LeftScores + " - " + RightScores;
    }

    void CheckWinCondition()
    {
        if (LeftScores >= 5)
        {
            scoreText.text = "¡GANASTE!";
            Time.timeScale = 0;

            retryButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
        else if (RightScores >= 5)
        {
            scoreText.text = "PERDISTE";
            Time.timeScale = 0;

            retryButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}













