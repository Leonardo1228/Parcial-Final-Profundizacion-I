using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int scoreValue = 10;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Bala
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
            Die();
            return;
        }

        // Jugador
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            GameManagerSEU.Instance.PlayerHit();
            Die();
        }
    }

    void Die()
    {
        GameManagerSEU.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}

