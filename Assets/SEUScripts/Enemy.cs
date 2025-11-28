using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int scoreValue = 10;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float shootRate = 2f;

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        InvokeRepeating(nameof(Shoot), 1f, shootRate);
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < Camera.main.ViewportToWorldPoint(Vector3.zero).y)
            Destroy(gameObject);
    }

    void Shoot()
    {
        if (player == null || enemyBulletPrefab == null) return;

        Vector3 dir = player.position - transform.position;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
            Die();
            return;
        }

        PlayerMovement playerHit = collision.GetComponent<PlayerMovement>();
        if (playerHit != null)
        {
            GameManagerSEU.Instance.PlayerHit();
            Die();
        }
    }

    void Die()
    {
        CancelInvoke();
        GameManagerSEU.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            GameManagerSEU.Instance.PlayerHit();
            Die();
        }
    }

}


