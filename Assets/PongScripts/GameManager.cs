using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ballPrefab;
    public Text scoreText;

    private int RightScores = 0;
    private int LeftScores = 0;

    private Ball currentBall; // Referencia a la bola actual

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
        SpawnBall();
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
        }
        else if (RightScores >= 5)
        {
            scoreText.text = "PERDISTE";
            Time.timeScale = 0;
        }
    }
}













