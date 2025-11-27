using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    ShootPlayerActions input;
    Vector2 moveInput;
    ScreenBounds bounds;

    void Awake()
    {
        input = new ShootPlayerActions();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        bounds = FindObjectOfType<ScreenBounds>();
    }

    void Update()
    {
        moveInput = input.Player.Move.ReadValue<Vector2>();

        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0)
                         * speed * Time.deltaTime;

        transform.position += movement;
        transform.position = bounds.Clamp(transform.position);
    }
}



