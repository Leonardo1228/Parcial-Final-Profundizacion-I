using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    ScreenBounds bounds;

    void Start()
    {
        bounds = FindObjectOfType<ScreenBounds>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position += movement;

        transform.position = bounds.Clamp(transform.position);
    }
}


