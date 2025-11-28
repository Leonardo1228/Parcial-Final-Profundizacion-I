using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPM : MonoBehaviour
{
    public static GameManagerPM Instance;

    [Header("Game Stats")]
    public int score = 0;
    public int lives = 3;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text livesText;

    [Header("References")]
    public PacController player;
    public GridSpawnerPM gridSpawner;

    [Header("Game Over")]
    public bool isGameOver = false;

    void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Chequeo referencias
        if (scoreText == null) Debug.LogWarning("GameManagerPM: scoreText no asignado.");
        if (livesText == null) Debug.LogWarning("GameManagerPM: livesText no asignado.");
        if (gridSpawner == null) Debug.LogWarning("GameManagerPM: gridSpawner no asignado.");
    }

    void Start()
    {
        UpdateUI();
    }

    // ================= SCORE =================
    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UpdateUI();
    }

    // ================= PLAYER HIT =================
    public void PlayerHit()
    {
        if (isGameOver) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
            GameOver();
        else
            RespawnPlayer();
    }

    // ================= RESPAWN =================
    void RespawnPlayer()
    {
        if (gridSpawner != null)
            gridSpawner.SpawnPlayer();
        else
            Debug.LogWarning("GameManagerPM: GridSpawnerPM no asignado.");
    }

    // ================= UI =================
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
        if (livesText != null)
            livesText.text = $"Lives: {lives}";
    }

    // ================= GAME OVER =================
    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER");

        if (gridSpawner != null)
            gridSpawner.ResetGhosts();

        Time.timeScale = 0f; // Pausar juego
    }

    // ================= RESTART GAME =================
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reactivar juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}




