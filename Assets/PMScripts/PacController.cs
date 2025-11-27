using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PacController : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask pelletLayer;
    public LayerMask ghostLayer;

    Rigidbody2D rb;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Este método lo llama automáticamente PlayerInput
    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + input * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Pellet por capa
        if (((1 << other.gameObject.layer) & pelletLayer) != 0)
        {
            Pellet p = other.GetComponent<Pellet>();
            if (p != null) p.Collect();
        }

        // Fantasma por capa
        if (((1 << other.gameObject.layer) & ghostLayer) != 0)
        {
            GameManagerPM.Instance?.PlayerHit();
        }
    }
}

