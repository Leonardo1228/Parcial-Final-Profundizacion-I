using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Duck : MonoBehaviour
{
    public float speed = 2f;
    public int points = 100;
    public bool isDead = false;
    Rigidbody2D rb;
    public Animator animator; // optional
    public float fallGravity = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 velocity, float rotation = 0f)
    {
        rb.linearVelocity = velocity;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        if (isDead)
        {
            // nothing else, let physics drop it
            return;
        }

        // Optional: flip sprite depending on velocity.x
        if (rb.linearVelocity.x > 0.1f) transform.localScale = new Vector3(1, 1, 1);
        else if (rb.linearVelocity.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Hit()
    {
        if (isDead) return;
        isDead = true;
        // stop horizontal movement, start falling
        rb.linearVelocity = new Vector2(0, -1f);
        rb.gravityScale = fallGravity;
        GetComponent<Collider2D>().enabled = false;
        if (animator) animator.SetTrigger("Die");
        GameManagerDH.I.AddScore(points);
        Destroy(gameObject, 3f); // cleanup
    }

    void OnBecameInvisible()
    {
        // if it flies off screen (not shot), player loses a life
        if (!isDead)
        {
            GameManagerDH.I.LoseLife();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

