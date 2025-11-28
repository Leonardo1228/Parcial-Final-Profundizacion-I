using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerT : MonoBehaviour
{
    public static GameManagerT Instance;

    [Header("Spawner")]
    public TetrisSpawner spawner;

    [Header("UI")]
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    int score = 0;
    bool isPaused = false;
    Transform currentPiece = null;
    bool spawningLocked = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);

        UpdateScoreUI();
        SpawnPiece();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void SpawnPiece()
    {
        if (spawningLocked || currentPiece != null) return;
        if (spawner == null)
        {
            Debug.LogError("GameManagerT: Spawner NO asignado");
            return;
        }

        spawningLocked = true;

        Transform spawned = spawner.SpawnNext();

        if (spawned != null)
            currentPiece = spawned;
        else
            GameOver();

        spawningLocked = false;
    }

    public void PieceLocked()
    {
        currentPiece = null;
        SpawnPiece();
    }

    public void AddScore(int linesCleared)
    {
        int points = 0;
        switch (linesCleared)
        {
            case 1: points = 100; break;
            case 2: points = 300; break;
            case 3: points = 500; break;
            case 4: points = 800; break;
        }

        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

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

























