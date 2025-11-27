using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        float top = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        if (transform.position.y > top)
            Destroy(gameObject);
    }
}


