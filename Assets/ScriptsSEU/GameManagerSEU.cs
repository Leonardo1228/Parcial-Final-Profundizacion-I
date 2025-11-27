using UnityEngine;

public class GameManagerSEU : MonoBehaviour
{
    public static GameManagerSEU Instance;

    [Header("Player")]
    public int playerLives = 3;

    [Header("Score")]
    public int score = 0;

    [Header("Spawner")]
    public EnemySpawner spawner;

    bool gameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        spawner.StartSpawning();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    public void PlayerHit()
    {
        if (gameOver) return;

        playerLives--;
        Debug.Log("Lives: " + playerLives);

        if (playerLives <= 0)
            GameOver();
    }

    void GameOver()
    {
        gameOver = true;
        spawner.StopSpawning();
        Debug.Log("GAME OVER");
    }
}

