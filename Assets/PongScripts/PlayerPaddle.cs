using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 10f;

    [Header("Muros")]
    public Transform superiorWall;
    public Transform inferiorWall;

    private Rigidbody2D rb;
    private PaddleInputActions inputActions;
    private float movementValue;

    private float upperLimit;
    private float lowerLimit;
    private float paddleHalfHeight;

    void Awake()
    {
        inputActions = new PaddleInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.movement.Vertical.performed += OnMove;
        inputActions.movement.Vertical.canceled += OnMove;
    }

    void OnDisable()
    {
        inputActions.movement.Vertical.performed -= OnMove;
        inputActions.movement.Vertical.canceled -= OnMove;
        inputActions.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleHalfHeight = transform.localScale.y / 2f;

        // Calcula límites basados en la posición y escala de los muros
        float upperWallHalfHeight = superiorWall.localScale.y / 2f;
        float lowerWallHalfHeight = inferiorWall.localScale.y / 2f;

        upperLimit = superiorWall.position.y - upperWallHalfHeight - paddleHalfHeight;
        lowerLimit = inferiorWall.position.y + lowerWallHalfHeight + paddleHalfHeight;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementValue = ctx.ReadValue<float>();
    }

    void Update()
    {
        // Calcula la nueva posición vertical
        float newY = rb.position.y + movementValue * speed * Time.deltaTime;

        // Limita entre los muros
        newY = Mathf.Clamp(newY, lowerLimit, upperLimit);

        rb.MovePosition(new Vector2(rb.position.x, newY));
    }
}





