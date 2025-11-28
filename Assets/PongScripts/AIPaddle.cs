using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float speed = 6f;

    [Header("Muros")]
    public Transform superiorWall;
    public Transform inferiorWall;

    private Ball ball;   // referencia al script Ball (no Tag)
    private Transform ballTransform;

    private float upperLimit;
    private float lowerLimit;
    private float paddleHalfHeight;

    void Start()
    {
        paddleHalfHeight = transform.localScale.y / 2f;

        float upperWallHalfHeight = superiorWall.localScale.y / 2f;
        float lowerWallHalfHeight = inferiorWall.localScale.y / 2f;

        upperLimit = superiorWall.position.y - upperWallHalfHeight - paddleHalfHeight;
        lowerLimit = inferiorWall.position.y + lowerWallHalfHeight + paddleHalfHeight;
    }

    void Update()
    {
        // 🔎 Busca la bola automáticamente si aún no existe referencia
        if (ball == null)
        {
            ball = FindAnyObjectByType<Ball>(); // NO usa tags
            if (ball != null)
                ballTransform = ball.transform;

            return;
        }

        float deadZone = 0.1f;
        float diff = ballTransform.position.y - transform.position.y;

        if (Mathf.Abs(diff) < deadZone)
            return;

        float direction = Mathf.Sign(diff);

        float newY = transform.position.y + direction * speed * Time.deltaTime;
        newY = Mathf.Clamp(newY, lowerLimit, upperLimit);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}



