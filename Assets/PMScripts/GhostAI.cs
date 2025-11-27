using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GhostAI : MonoBehaviour
{
    public float speed = 3f;
    public LayerMask wallLayer;

    Rigidbody2D rb;
    Vector2 dir;
    float changeInterval = 1.2f;
    float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseDirection();
        timer = changeInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            ChooseDirection();
            timer = changeInterval;
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void ChooseDirection()
    {
        Vector2[] options = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        dir = options[Random.Range(0, options.Length)];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detectar muro por CAPA
        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            dir = -dir;
        }

        // Detectar jugador por SCRIPT
        if (other.GetComponent<PacController>() != null)
        {
            GameManagerPM.Instance?.PlayerHit();
        }
    }
}


