using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerDH : MonoBehaviour
{
    public static GameManagerDH I;
    public int score;
    public int lives = 3;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public float roundTime = 30f;
    float timer;

    void Awake()
    {
        if (I == null) I = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = roundTime;
        UpdateUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            NextRound();
        }
    }

    public void AddScore(int s)
    {
        score += s;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"PUNTOS: {score}";
        if (livesText) livesText.text = $"VIDAS: {lives}";
    }

    void NextRound()
    {
        // simple: reset timer, can increase difficulty
        timer = roundTime;
        // Could signal Spawner to increase spawn rate
        BroadcastMessage("OnNextRound", SendMessageOptions.DontRequireReceiver);
    }

    void GameOver()
    {
        // For prototype just reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

