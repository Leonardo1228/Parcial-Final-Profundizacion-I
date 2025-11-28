using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Velocidad")]
    public float initialSpeed = 6f;
    public float speedIncrease = 0.5f;
    public float maxSpeed = 15f;

    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        LaunchBall();
    }

    void LaunchBall()
    {
        transform.position = Vector2.zero;

        float x = Random.value < 0.5f ? -1f : 1f;
        float y = Random.Range(-0.5f, 0.5f);

        direction = new Vector2(x, y).normalized;
        rb.linearVelocity = direction * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string name = collision.collider.gameObject.name;

        // Rebote paredes
        if (name == "SuperiorWall" || name == "InferiorWall")
        {
            direction = new Vector2(direction.x, -direction.y).normalized;
            rb.linearVelocity = direction * rb.linearVelocity.magnitude;
            return;
        }

        // Rebote paddles
        if (name == "PlayerPaddle" || name == "AIPaddle")
        {
            float paddleHeight = collision.collider.bounds.size.y;
            float impactPoint = (transform.position.y - collision.transform.position.y) / (paddleHeight / 2);

            float bounceAngle = impactPoint * 75f;
            float rad = bounceAngle * Mathf.Deg2Rad;

            float xDir = name == "PlayerPaddle" ? 1f : -1f;

            direction = new Vector2(xDir * Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            float newSpeed = Mathf.Min(rb.linearVelocity.magnitude + speedIncrease, maxSpeed);
            rb.linearVelocity = direction * newSpeed;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleGoal(other.name);
    }

    private void HandleGoal(string name)
    {
        if (name == "LeftGoal")
        {
            GameManagerP.Instance.PlayerScoresPoint();
        }
        else if (name == "RightGoal")
        {
            GameManagerP.Instance.AIScoresPoint();
        }
    }

    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // Si sale por la izquierda
        if (viewPos.x < 0f)
        {
            GameManagerP.Instance.AIScoresPoint();
            ResetBall();
            return;
        }

        // Si sale por la derecha
        if (viewPos.x > 1f)
        {
            GameManagerP.Instance.PlayerScoresPoint();
            ResetBall();
            return;
        }
    }
    public void ResetBall()
    {
        LaunchBall();
    }
}




















