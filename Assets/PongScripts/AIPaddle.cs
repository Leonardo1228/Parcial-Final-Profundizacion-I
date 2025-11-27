using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float speed = 6f;
    public Transform ball;

    [Header("Muros")]
    public Transform superiorWall;
    public Transform inferiorWall;

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
        if (ball == null)
            return;

        float deadZone = 0.1f;
        float diff = ball.position.y - transform.position.y;

        if (Mathf.Abs(diff) < deadZone)
            return;

        float direction = Mathf.Sign(diff);

        // Movimiento calculado
        float newY = transform.position.y + direction * speed * Time.deltaTime;

        // Limita entre los muros
        newY = Mathf.Clamp(newY, lowerLimit, upperLimit);

        // Aplica el movimiento sin alterar la lógica
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}



