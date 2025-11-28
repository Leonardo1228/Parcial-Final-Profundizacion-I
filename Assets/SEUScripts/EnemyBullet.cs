using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 6f;
    Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        Vector3 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        if (transform.position.y < bottom.y - 1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            GameManagerSEU.Instance.PlayerHit();
            Destroy(gameObject);
        }
    }
}


