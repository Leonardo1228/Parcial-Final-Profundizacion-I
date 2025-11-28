using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSEU : MonoBehaviour
{
    public static GameManagerSEU Instance;

    [Header("Player")]
    public int playerLives = 3;

    [Header("Spawner")]
    public EnemySpawner spawner;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    [Header("Score")]
    public int score = 0;

    bool gameOver = false;
    bool paused = false;

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;

        // BLINDAJE ABSOLUTO
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        gameOver = false;
        paused = false;

        if (spawner != null)
            spawner.StartSpawning();
    }

    void Update()
    {
        // PAUSA CON ENTER
        if (!gameOver && Input.GetKeyDown(KeyCode.Return))
        {
            TogglePause();
        }
    }

    // ------------------ GAME OVER ------------------

    public void PlayerHit()
    {
        if (gameOver) return;

        playerLives--;

        if (playerLives <= 0)
            GameOver();
    }

    void GameOver()
    {
        gameOver = true;

        if (spawner != null)
            spawner.StopSpawning();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    // ------------------ PAUSA ------------------

    public void TogglePause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0f;
            if (pausePanel != null)
                pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // ------------------ BOTONES ------------------

    public void Retry()
    {
        if (spawner != null)
            spawner.StopSpawning();

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        if (spawner != null)
            spawner.StopSpawning();

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}




