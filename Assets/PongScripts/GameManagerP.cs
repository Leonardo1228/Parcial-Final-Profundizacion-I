using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerP : MonoBehaviour
{
    public static GameManagerP Instance;

    [Header("Ball")]
    public GameObject ballPrefab;
    private Ball currentBall;

    [Header("Score")]
    public Text scoreText;
    private int RightScores = 0;
    private int LeftScores = 0;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        UpdateScoreText();
        SpawnBall();

        // Ocultar panels al inicio
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Tecla ENTER para pausar
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void SpawnBall()
    {
        if (currentBall != null)
            return;

        GameObject ballObj = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        currentBall = ballObj.GetComponent<Ball>();
    }

    void ResetCurrentBall()
    {
        if (currentBall != null)
            currentBall.ResetBall();
    }

    // ==============================
    // ✅ PUNTAJES
    // ==============================

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

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = LeftScores + " - " + RightScores;
    }

    void CheckWinCondition()
    {
        if (LeftScores >= 5)
        {
            ShowGameOver("¡GANASTE!");
        }
        else if (RightScores >= 5)
        {
            ShowGameOver("PERDISTE");
        }
    }

    void ShowGameOver(string message)
    {
        if (scoreText != null)
            scoreText.text = message;

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    // ==============================
    // ✅ PAUSA
    // ==============================

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // ==============================
    // ✅ BOTONES
    // ==============================

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}













