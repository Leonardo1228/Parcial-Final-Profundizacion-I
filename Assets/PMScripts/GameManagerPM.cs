using UnityEngine;
using UnityEngine.UI;

public class GameManagerPM : MonoBehaviour
{
    public static GameManagerPM Instance;

    public int score = 0;
    public int lives = 3;

    [Header("UI")]
    public Text scoreText;
    public Text livesText;

    [Header("References")]
    public PacController player;
    public GridSpawnerPM gridSpawner;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    // ================= SCORE =================
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    // ================= PLAYER HIT =================
    public void PlayerHit()
    {
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer();
        }
    }

    // ================= RESPAWN =================
    void RespawnPlayer()
    {
        if (gridSpawner != null)
        {
            gridSpawner.SpawnPlayer();
        }
        else
        {
            Debug.LogWarning("GameManagerPM: GridSpawnerPM no asignado.");
        }
    }

    // ================= UI =================
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    // ================= GAME OVER =================
    void GameOver()
    {
        Debug.Log("GAME OVER");

        if (gridSpawner != null)
            gridSpawner.ResetGhosts();

        Time.timeScale = 0f;
    }
}


