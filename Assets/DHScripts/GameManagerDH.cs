using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerDH : MonoBehaviour
{
    public static GameManagerDH I;

    [Header("Game State")]
    public int score;
    public int lives = 3;
    public float roundTime = 30f;
    float timer;
    bool roundActive;

    [Header("References")]
    public DuckSpawner duckSpawner;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text roundText;

    int roundNumber = 1;

    void Awake()
    {
        if (I == null) I = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartRound();
        UpdateUI();
    }

    void Update()
    {
        if (!roundActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EndRound();
        }
    }

    void StartRound()
    {
        roundActive = true;
        timer = roundTime;

        if (roundText)
            roundText.text = "RONDA " + roundNumber;

        duckSpawner.StartSpawning();
    }

    void EndRound()
    {
        roundActive = false;
        duckSpawner.StopSpawning();

        roundNumber++;
        duckSpawner.IncreaseDifficulty();

        Invoke(nameof(StartRound), 2f); // pausa entre rondas
    }

    public void AddScore(int s)
    {
        score += s;
        UpdateUI();
    }

    public void LoseLife()
    {
        if (!roundActive) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
            GameOver();
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"PUNTOS: {score}";
        if (livesText) livesText.text = $"VIDAS: {lives}";
    }

    void GameOver()
    {
        duckSpawner.StopSpawning();

        if (roundText)
            roundText.text = "GAME OVER";

        Invoke(nameof(Reload), 3f);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

